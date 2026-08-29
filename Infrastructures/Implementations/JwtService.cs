using BasicAuthApi.Infrastructures.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicAuthApi.Infrastructures.Implementations;

public class JwtService(JwtConfiguration jwt) : IJwtService
{
    private readonly JwtConfiguration jwt = jwt;

    public string GetAccessToken(IEnumerable<Claim> claims)
    {
        DateTime now = DateTime.Now;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwt.Key));
        SigningCredentials signingCredentials = new(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            jwt.Issuer,
            jwt.Audience,
            notBefore: now,
            expires: now.AddMinutes(jwt.ExpiresInMin),
            claims: claims,
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
