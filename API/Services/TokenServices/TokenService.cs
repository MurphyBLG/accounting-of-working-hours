using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    public string BuildToken(Employee employee, string key, string issuer, string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)); // ?????
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // ?????

        var claims = new[]
        {
            new Claim("EmployeeId", employee.EmployeeId.ToString()),
        };

        var token = new JwtSecurityToken (
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}