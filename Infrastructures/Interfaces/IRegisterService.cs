using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface IRegisterService
{
    Task RegisterAsync(RegisterDto dto);
}
