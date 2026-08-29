using Microsoft.AspNetCore.Identity;

namespace BasicAuthApi.Infrastructures.Errors;

public static class ErrorChecker
{
    public static void ThrowIfNotSucceeded(this IdentityResult result)
    {
        if (!result.Succeeded)
        {
            ArgumentException exception = new(string.Join(", ", result.Errors.Select(e => e.Description)));
            throw exception;
        }
    }
}
