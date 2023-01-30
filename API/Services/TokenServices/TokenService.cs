using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddMinutes(30)
        };

        return refreshToken;
    }

    public Employee? GetCurrentEmployee(ClaimsPrincipal User, AccountingOfWorkingHoursContext context)
    {
        if (User.FindFirstValue("EmployeeId") is null)
        {
            return null;
        }

        Guid employeeId = new Guid(User.FindFirstValue("EmployeeId")!);

        Employee? currentEmployee = context.Employees.Find(employeeId);

        if (currentEmployee is null)
        {
            return null;
        }

        return currentEmployee;
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, IConfiguration config)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}