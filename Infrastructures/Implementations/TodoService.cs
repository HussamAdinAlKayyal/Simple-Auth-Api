using BasicAuthApi.Infrastructures.Data;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using BasicAuthApi.Models.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicAuthApi.Infrastructures.Implementations;

public class TodoService(ApplicationDbContext context) : ITodoService
{
    private readonly ApplicationDbContext context = context;

    public async Task<TodoDetailsDto> AddTodoAsync(string userId, CreateTodoDto dto)
    {
        Todo todo = new()
        {
            Title = dto.Title,
            UserId = userId,
            CreatedAt = DateTime.Now,
        };
        if (dto.Description is not null)
        {
            todo.Description = dto.Description;
        }
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return todo.AsTodoDetailsDto();
    }
    public async Task<IEnumerable<TodoDetailsDto>> AddTodosAsync(string userId, IEnumerable<CreateTodoDto> dtos)
    {
        List<Todo> todoList = [];
        foreach (CreateTodoDto dto in dtos)
        {
            todoList.Add(GetTodo(dto, userId));
        }
        await context.Todos.AddRangeAsync(todoList);
        await context.SaveChangesAsync();
        return todoList.Select(t => t.AsTodoDetailsDto());
    }

    public async Task DeleteTodoAsync(int todoId)
    {
        Todo todo = await FindAsync(todoId);
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int todoId, string userId)
    {
        await ThrowIfTodoNotForUserAsync(todoId, userId);
        await DeleteTodoAsync(todoId);
    }

    public async Task<IEnumerable<TodoDetailsDto>> GetTodosAsync(string userId)
    {
        return await context.Todos.AsNoTracking().Where(t => t.UserId == userId).Select(t => t.AsTodoDetailsDto()).ToArrayAsync();
    }

    public async Task<IEnumerable<TodoDetailsDto>> GetTodosAsync()
    {
        return await context.Todos.AsNoTracking().Select(t => t.AsTodoDetailsDto()).ToArrayAsync();
    }

    public async Task<TodoDetailsDto> GetTodoAsync(int todoId, string userId)
    {
        await ThrowIfTodoNotForUserAsync(todoId, userId);
        return (await FindAsync(todoId)).AsTodoDetailsDto();
    }

    public async Task UpdateTodoAsync(int todoId, string userId, UpdateTodoDto dto)
    {
        await ThrowIfTodoNotForUserAsync(todoId, userId);
        Todo todo = await FindAsync(todoId);
        if (dto.Title is not null)
        {
            todo.Title = dto.Title;
        }
        if (dto.Description is not null)
        {
            todo.Description = dto.Description;
        }
        if (dto.IsCompleted.HasValue)
        {
            todo.CompletedAt = DateTime.Now;
        }
        EntityEntry<Todo> entity = context.Todos.Entry(todo);
        if (entity.State == EntityState.Modified)
        {
            todo.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }

    private async Task ThrowIfTodoNotForUserAsync(int todoId, string userId)
    {
        if (!await context.Todos.AnyAsync(t => t.UserId == userId && t.Id == todoId))
            throw new UnauthorizedAccessException("You don't have the ability to do the action, due to either you don't have this todo, or you are not registed, or both");
    }

    private async Task<Todo> FindAsync(int id) => 
        await context.Todos.FindAsync(id) ?? throw new KeyNotFoundException($"No todo with id equals to {id}");

    private static Todo GetTodo(CreateTodoDto dto, string userId)
    {
        Todo todo = new()
        {
            Title = dto.Title,
            UserId = userId,
            CreatedAt = DateTime.Now,
        };
        if (dto.Description is not null)
        {
            todo.Description = dto.Description;
        }
        return todo;
    }
}
