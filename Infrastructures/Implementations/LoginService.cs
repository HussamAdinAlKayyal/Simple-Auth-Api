using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BasicAuthApi.Infrastructures.Implementations
{
    public class LoginService(UserManager<User> userManager, IJwtService jwtService) : ILoginService
    {
        private enum LoginUsing 
        { 
            Email, 
            Username 
        }

        private readonly UserManager<User> userManager = userManager;

        private readonly IJwtService jwtService = jwtService;

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
                throw new ArgumentException($"{loginUsing} or password is wrong, recheck them out.");
            }

            IEnumerable<Claim> claims = await user.GetClaimsAsync(userManager);

            return jwtService.GetAccessToken(claims);
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
