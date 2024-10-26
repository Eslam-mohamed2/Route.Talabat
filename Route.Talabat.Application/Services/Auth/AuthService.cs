using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core.Application.Abstraction.Models.Auth;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Application.Exceptions;
using Route.Talabat.Core.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Route.Talabat.Core.Application.Services.Auth
{
    public class AuthService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var User = await userManager.FindByEmailAsync(model.Email);
            if (User is null) throw new UnAuthorizedException("Invalid Login");
            var Result = await signInManager.CheckPasswordSignInAsync(User, model.Password, lockoutOnFailure: true);

            if (!Result.IsNotAllowed) throw new UnAuthorizedException("Account not Confirmed yet");
            if (!Result.IsLockedOut) throw new UnAuthorizedException("Account Is locked");
            if (!Result.Succeeded) throw new UnAuthorizedException("Invalid Login");

            var Response = new UserDto()
            {
                Id = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await GenerateTokenAsync(User, _jwtSettings)
            };
            return Response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var User = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };
            var Result = await userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded) throw new ValidationException() { Errors = Result.Errors.Select(E => E.Description) };
            var Response = new UserDto()
            {
                Id = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await GenerateTokenAsync(User, _jwtSettings)
            };
            return Response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user, JwtSettings _jwtSettings)
        {
            var UserClaims = await userManager.GetClaimsAsync(user);
            var rolesAsClaims = new List<Claim>();
            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var Role in UserRoles)
            {
                rolesAsClaims.Add(new Claim(ClaimTypes.Role, Role.ToString()));
            }

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.DisplayName)

            }.Union(UserClaims)
            .Union(rolesAsClaims)
            .ToList();

            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var TokenObj = new JwtSecurityToken(
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_jwtSettings.DurationInMinutes)),
                claims: Claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(TokenObj);
        }
    }
}
