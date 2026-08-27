using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Infrastructures.Implementations;

public class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> userManager = userManager;

    public Task<IEnumerable<UserDetailsDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<UserDetailsDto> GetUserByIdAsync(string id)
    {
        User? user = await userManager.FindByIdAsync(id);
        return user == null
            ? throw new Exception("No user with this id!")
            : new UserDetailsDto(user.UserName!, user.FirstName, user.LastName, user.Email!);
    }
}
