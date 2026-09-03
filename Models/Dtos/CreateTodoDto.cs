using System.ComponentModel.DataAnnotations;

namespace BasicAuthApi.Models.Dtos;

public record CreateTodoDto(
    [Required, StringLength(128)] string Title,
    [StringLength(1000)] string? Description);
