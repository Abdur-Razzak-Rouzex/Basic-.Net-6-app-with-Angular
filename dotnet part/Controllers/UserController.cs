using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<ServiceResponse<String>>> AuthenticateUser(UserLoginDto loginInfo)
        {
            if (loginInfo is null)
            {
                return BadRequest();
            }

            var response = await _userService.AuthenticateUser(loginInfo);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> SignupUser(UserSignupDto signupInfo)
        {
            if (signupInfo.Username is null && signupInfo.Password is null)
            {
                return BadRequest();
            }

            var response = await _userService.SignupUser(signupInfo);
            return Ok(response);
        }
    }
}