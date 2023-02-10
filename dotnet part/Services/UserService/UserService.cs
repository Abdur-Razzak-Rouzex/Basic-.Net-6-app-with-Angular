using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public readonly DataContext _context;
        public UserService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<String>> AuthenticateUser(UserLoginDto loginInfo)
        {
            var serviceResponse = new ServiceResponse<String>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginInfo.Username && u.Password == loginInfo.Password);

                if (user is null)
                {
                    throw new Exception("Username or password is wrong");
                }

                serviceResponse.Message = "User login success!";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<GetUserDto>> SignupUser(UserSignupDto signupInfo)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            var newUser = _mapper.Map<User>(signupInfo);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetUserDto>(newUser);
            serviceResponse.Message = "User created successfully";

            return serviceResponse;
        }
    }
}