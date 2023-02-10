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
        public async Task<ActionResult<ServiceResponse<String>>> Authenticate(UserLoginDto loginInfo)
        {
            if (loginInfo is null)
            {
                return BadRequest();
            }

            var response = await _userService.Authenticate(loginInfo);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}