using System.Security.Claims;

namespace BasicAuthApi.Infrastructures.Interfaces;

public interface IJwtService
{
    string GetAccessToken(string issuer, string audience, string key, double expiresInMin, params Claim[] claims);
}
