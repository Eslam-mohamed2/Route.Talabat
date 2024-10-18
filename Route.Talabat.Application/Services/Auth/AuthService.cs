using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Application.Abstraction.Models.Auth;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Application.Exceptions;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Auth
{
    internal class AuthService(IAuthService authService,UserManager<ApplicationUser> _userManger , SignInManager<ApplicationUser> signInManger) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            if (user is null) throw new BadRequestException("Invalid Login");

            var result = await signInManger.CheckPasswordSignInAsync(user ,model.Password,lockoutOnFailure: true );

            if (!result.Succeeded) throw new BadRequestException("Invalid Login");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This Will be JWT Token"
            };

            return response;
        }
        

        public async Task<UserDto> LogoutAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManger.CreateAsync(user,model.Password);

            if (!result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };


            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This Will be JWT Token"
            };

            return response;
        }
    }
}
