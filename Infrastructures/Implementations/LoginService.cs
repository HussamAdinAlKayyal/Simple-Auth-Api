using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Infrastructures.Implementations
{
    public class LoginService(UserManager<User> userManager, IJwtService jwtService, IConfiguration configuration) : ILoginService
    {
        private readonly UserManager<User> userManager = userManager;

        private readonly IJwtService jwtService = jwtService;

        private readonly IConfiguration configuration = configuration;

        public async Task<string> LoginUsingEmailAsync(LoginUsingEmailDto dto)
        {
            User? user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new Exception("Email or password is wrong, recheck them.");
            }

            string? issuer = configuration["Jwt:Issuer"],
                    audience = configuration["Jwt:Audience"],
                    key = configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
            {
                throw new InternalServerException("Configuration error, try again later.");
            }

            return jwtService.GetAccessToken(
                issuer,
                audience,
                key,
                [
                    new("username", user.UserName!),
                    new("firstName", user.FirstName),
                    new("lastName", user.LastName),
                    new("Email", user.Email!)
                ]
            );
        }
    }
}
