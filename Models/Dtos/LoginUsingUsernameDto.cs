using System.ComponentModel.DataAnnotations;

namespace BasicAuthApi.Models.Dtos;

public record LoginUsingUsernameDto(
    [Required, StringLength(100, MinimumLength = 4)] string Username,
    [Required, StringLength(100, MinimumLength = 6)] string Password);
