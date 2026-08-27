using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Models;

public class User(string firstName, string lastName) : IdentityUser
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}
