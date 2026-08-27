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

        private enum LoginUsing { Email, Username }

        private async Task<string> LoginAsync(string usernameOrEmail, string password, LoginUsing loginUsing)
        {
            User? user = loginUsing switch
            {
                LoginUsing.Email => await userManager.FindByEmailAsync(usernameOrEmail),
                LoginUsing.Username => await userManager.FindByNameAsync(usernameOrEmail),
                _ => throw new NotImplementedException(),
            };

            if (user == null || !await userManager.CheckPasswordAsync(user, password))
            {
                throw new Exception($"{loginUsing} or password is wrong, recheck them.");
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
                30,
                [
                    new("username", user.UserName!),
                    new("firstName", user.FirstName),
                    new("lastName", user.LastName),
                    new("Email", user.Email!)
                ]
            );
        }

        public async Task<string> LoginUsingEmailAsync(LoginUsingEmailDto dto)
        {
            return await LoginAsync(dto.Email, dto.Password, LoginUsing.Email);
        }

        public async Task<string> LoginUsingUsernameAsync(LoginUsingUsernameDto dto)
        {
            return await LoginAsync(dto.Username, dto.Password, LoginUsing.Username);
        }
    }
}
