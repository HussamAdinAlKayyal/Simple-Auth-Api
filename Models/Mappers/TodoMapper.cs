using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Models.Mappers;

public static class TodoMapper
{
    public static TodoDetailsDto AsTodoDetailsDto(this Todo todo)
    {
        return new TodoDetailsDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.CreatedAt,
            todo.CompletedAt is not null,
            todo.CompletedAt);
    }
}
