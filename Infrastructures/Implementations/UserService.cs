using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using BasicAuthApi.Models.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi.Infrastructures.Implementations;

internal class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> userManager = userManager;

    public async Task<IEnumerable<UserDetailsDto>> GetAllAsync()
    {
        return await userManager.Users.Select(u => u.AsUserDetailsDto()).ToListAsync();
    }
    
    public async Task<UserDetailsDto> GetUserByIdAsync(string id)
    {
        User? user = await userManager.FindByIdAsync(id);
        return user is null
            ? throw new KeyNotFoundException("No user with this id!")
            : user.AsUserDetailsDto();
    }

    public async Task AddRoleToUserAsync(string id, string role)
    {
        User user = await userManager.FindByIdAsync(id) ?? throw new KeyNotFoundException($"No user with id equals to {id}");
        IdentityResult result = await userManager.AddToRoleAsync(user, role);
        result.ThrowIfNotSucceeded();
    }
}
