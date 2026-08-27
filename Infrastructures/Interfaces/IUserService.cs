using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDetailsDto>> GetAllAsync();
    Task<UserDetailsDto> GetUserByIdAsync(string id);
}
