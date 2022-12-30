using API.Models;

public interface ITokenService
{
    string BuildToken(Employee employee, string key, string issuer, string audience);
}