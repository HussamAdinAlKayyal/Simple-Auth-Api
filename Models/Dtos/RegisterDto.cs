using System.ComponentModel.DataAnnotations;

namespace BasicAuthApi.Models.Dtos;

public record RegisterDto(
    [Required, StringLength(100, MinimumLength = 4)] string Username,
    [Required, StringLength(100, MinimumLength = 1)] string FirstName,
    [Required, StringLength(100, MinimumLength = 1)] string LastName,
    [Required, EmailAddress] string Email,
    [Required, StringLength(100, MinimumLength = 6)] string Password,
    [Required, StringLength(100, MinimumLength = 6)] string ConfirmPassword);
