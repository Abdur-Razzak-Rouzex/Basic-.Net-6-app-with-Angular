using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<UserLoginDto>> Authenticate(UserLoginDto loginInfo);
    }
}