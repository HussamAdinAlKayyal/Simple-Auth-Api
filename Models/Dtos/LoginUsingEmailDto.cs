using System.ComponentModel.DataAnnotations;

namespace BasicAuthApi.Models.Dtos;

public record LoginUsingEmailDto(
    [Required, EmailAddress] string Email,
    [Required, StringLength(100, MinimumLength = 6)] string Password);
    