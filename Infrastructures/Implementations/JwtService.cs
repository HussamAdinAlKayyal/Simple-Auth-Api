using BasicAuthApi.Infrastructures.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicAuthApi.Infrastructures.Implementations;

public class JwtService : IJwtService
{
    public string GetAccessToken(string issuer, string audience, string key, params Claim[] claims)
    {
        DateTime now = DateTime.Now;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingCredentials = new(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer,
            audience,
            notBefore: now,
            claims: claims,
            expires: now.AddMinutes(3),
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
