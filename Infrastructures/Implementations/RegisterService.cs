using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Infrastructures.Implementations;

public class RegisterService(UserManager<User> userManager) : IRegisterService
{
    private readonly UserManager<User> userManager = userManager;

    public async Task RegisterAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            throw new Exception("The password is not identical to the password confirm!");
        }
        User user = new(dto.FirstName, dto.LastName)
        {
            Email = dto.Email,
            UserName = dto.Email,
        };
        IdentityResult result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
