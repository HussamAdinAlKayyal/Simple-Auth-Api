using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface ITodoService
{
    Task<TodoDetailsDto> AddTodoAsync(string userId, CreateTodoDto dto);
    Task<IEnumerable<TodoDetailsDto>> AddTodosAsync(string userId, IEnumerable<CreateTodoDto> dtos);
    Task DeleteTodoAsync(int todoId);
    Task DeleteTodoAsync(int todoId, string userId);
    Task<TodoDetailsDto> GetTodoAsync(int todoId, string userId);
    Task<IEnumerable<TodoDetailsDto>> GetTodosAsync();
    Task<IEnumerable<TodoDetailsDto>> GetTodosAsync(string userId);
    Task UpdateTodoAsync(int todoId, string userId, UpdateTodoDto dto);
}
