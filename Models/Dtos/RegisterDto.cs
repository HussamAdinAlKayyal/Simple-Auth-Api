namespace BasicAuthApi.Models.Dtos;

public record RegisterDto(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
