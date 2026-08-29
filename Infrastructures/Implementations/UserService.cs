using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi.Infrastructures.Implementations;

public class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> userManager = userManager;

    public async Task<IEnumerable<UserDetailsDto>> GetAllAsync()
    {
        return await userManager.Users.Select(u => new UserDetailsDto(u.UserName!, u.FirstName, u.LastName, u.Email!)).ToListAsync();
    }
    
    public async Task<UserDetailsDto> GetUserByIdAsync(string id)
    {
        User? user = await userManager.FindByIdAsync(id);
        return user == null
            ? throw new KeyNotFoundException("No user with this id!")
            : new UserDetailsDto(user.UserName!, user.FirstName, user.LastName, user.Email!);
    }

    public async Task AddRoleToUserAsync(string id, string role)
    {
        User user = await userManager.FindByIdAsync(id) ?? throw new KeyNotFoundException($"No user with id equals to {id}");
        IdentityResult result = await userManager.AddToRoleAsync(user, role);
        result.ThrowIfNotSucceeded();
    }
}
