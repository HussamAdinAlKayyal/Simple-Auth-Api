using System.ComponentModel.DataAnnotations;

namespace BasicAuthApi.Models.Dtos;

public record UpdateTodoDto(
    [StringLength(128)] string? Title,
    [StringLength(1000)] string? Description,
    bool? IsCompleted);
