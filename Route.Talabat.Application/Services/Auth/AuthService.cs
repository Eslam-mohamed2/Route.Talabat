using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core.Application.Abstraction.Models.Auth;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Application.Exceptions;
using Route.Talabat.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Auth
{
    internal class AuthService(IAuthService authService,UserManager<ApplicationUser> _userManger , SignInManager<ApplicationUser> signInManger ,IOptions<JwtSettings> JwtSettings) : IAuthService
    {

        public readonly JwtSettings _JwtSettings = JwtSettings.Value;
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
                Token = await GenerateTokenAsync(user),
            };

            return response;
        }
        
        public async Task<UserDto> RegisterAsync(RegisterDto model)
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

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var privateClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            }
            .Union(await _userManger.GetClaimsAsync(user)).ToList();

            foreach (var role in await _userManger.GetRolesAsync(user))
            {
             privateClaims.Add(new Claim(ClaimTypes.Role,role.ToString()));   
            }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.Key));

            var tokenObj = new JwtSecurityToken(

                audience: _JwtSettings.Audience,
                issuer: _JwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_JwtSettings.DurationInMinutes),
                claims: privateClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
    }
}
