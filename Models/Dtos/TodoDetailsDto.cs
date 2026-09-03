namespace BasicAuthApi.Models.Dtos;

public record TodoDetailsDto(
    int Id,
    string Title,
    string Description,
    DateTime CreatedAt,
    bool IsCompleted,
    DateTime? CompletedAt);
