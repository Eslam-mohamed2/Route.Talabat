using Microsoft.AspNetCore.Mvc;
using Route.Talaat.APIs.Controllers.Base;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talabat.Core.Application.Abstraction.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManger serviceManager) : BaseApiController
    {
        [HttpPost("login")] // Post: /api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var response = await serviceManager.AuthService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var response = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(response);
        }

    }
}
