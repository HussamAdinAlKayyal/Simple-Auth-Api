using BasicAuthApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace BasicAuthApi.Infrastructures.Implementations;

public class PasswordHasher : IPasswordHasher<User>
{
    public string HashPassword(User user, string password)
    {
        return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        return hashedPassword == HashPassword(user, providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}
