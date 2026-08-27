namespace BasicAuthApi.Models.Dtos;

public record RegisterDto(string Username, string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
