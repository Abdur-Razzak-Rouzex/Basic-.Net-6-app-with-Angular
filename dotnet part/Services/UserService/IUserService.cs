using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<String>> AuthenticateUser(UserLoginDto loginInfo);
        Task<ServiceResponse<GetUserDto>> SignupUser(UserSignupDto signupInfo);
    }
}