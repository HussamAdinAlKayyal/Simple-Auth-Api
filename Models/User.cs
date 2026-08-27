using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User(string userName, string firstName, string lastName, string email)
    {
        UserName = userName;
        FirstName = firstName; 
        LastName = lastName;
        Email = email;
    }
}
