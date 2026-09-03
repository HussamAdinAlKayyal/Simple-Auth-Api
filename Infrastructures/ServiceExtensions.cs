using BasicAuthApi.Models;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BasicAuthApi.Infrastructures;

internal static class ServiceExtensions
{
    private static readonly string usernameTypeName = ClaimTypes.Name;

    private static readonly string firstNameTypeName = "firstName";

    private static readonly string lastNameTypeName = "lastName";

    private static readonly string emailTypeName = ClaimTypes.Email;

    private static readonly string roleTypeName = ClaimTypes.Role;
    
    private static readonly string idTypeName = ClaimTypes.NameIdentifier;

    public static async Task<IEnumerable<Claim>> GetClaimsAsync(this User user, UserManager<User> userManager)
    {
        List<Claim> claims = [
                    new(idTypeName, user.Id),
                    new(usernameTypeName, user.UserName!),
                    new(firstNameTypeName, user.FirstName),
                    new(lastNameTypeName, user.LastName),
                    new(emailTypeName, user.Email!)];
        claims.AddRange((await userManager.GetRolesAsync(user)).Select(s => new Claim(roleTypeName, s)));
        return claims;
    }

    public static UserDetailsDto MapClaimsToUserDetails(this ClaimsPrincipal user)
    {
        IEnumerable<Claim> claims = user.Claims;
        claims = claims.Where(c => c.Type != roleTypeName);
        Dictionary<string, string> dict = claims.ToDictionary(c => c.Type, c => c.Value);
        return new(
            dict[idTypeName],
            dict[usernameTypeName],
            dict[firstNameTypeName],
            dict[lastNameTypeName],
            dict[emailTypeName]);
    }

    public static string GetUserId(this ClaimsPrincipal user) => user.Claims.First(c => c.Type == idTypeName).Value;

    public static void AssignIfNotNull<T>(ref T first, T? second) where T : notnull
    {
        if (second is not null)
        {
            first = second;
        }
    }
}
