using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface ILoginService
{
    Task<string> LoginUsingEmailAsync(LoginUsingEmailDto dto);
}
