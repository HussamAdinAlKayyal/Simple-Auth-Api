using System.Security.Claims;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface IJwtService
{
    string GetAccessToken(IEnumerable<Claim> claims);
}
