using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BasicAuthApi.Infrastructures.Implementations;

public class RegisterService(UserManager<User> userManager, IJwtService jwtService) : IRegisterService
{
    private readonly UserManager<User> userManager = userManager;
    
    private readonly IJwtService jwtService = jwtService;

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            throw new ArgumentException("The password is not identical to the password confirm!");
        }
        User user = new(dto.Username, dto.FirstName, dto.LastName, dto.Email);

        IdentityResult result = await userManager.CreateAsync(user, dto.Password);
        result.ThrowIfNotSucceeded();
        result = await userManager.AddToRoleAsync(user, "User");
        result.ThrowIfNotSucceeded();

        IEnumerable<Claim> claims = await user.GetClaimsAsync(userManager);

        return jwtService.GetAccessToken(claims);
    }
}
