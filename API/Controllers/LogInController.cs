using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]")]
public class LogInController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private IConfiguration _config;

    public LogInController(AccountingOfWorkingHoursContext context, IConfiguration config)
    {
        this._context = context;
        this._config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult AuthorizeEmployee([FromBody] EmployeeLogInDTO employeeLogInDTO)
    {
        string? token = GenerateToken(employeeLogInDTO);

        if (token == null)
        {
            return BadRequest("Authentication error: There is no such user");
        }

        return Ok(token);
    }

    private EmployeeAuthenticationDTO? AuthenticateEmployee(EmployeeLogInDTO employeeLogInDTO)
    {
        Employee? current_employee = _context.Employees
            .Include(e => e.Position)
            .SingleOrDefault(e => e.EmployeeId == employeeLogInDTO.Password);

        if (current_employee == null)
        {
            return null;
        }

        Position current_employee_position = current_employee.Position!;

        return new EmployeeAuthenticationDTO
        {
            EmployeeId = employeeLogInDTO.Password,
            InterfaceAccesses = current_employee_position.InterfaceAccesses
        };
    }

    private string? GenerateToken(EmployeeLogInDTO employeeLogInDTO)
    {
        EmployeeAuthenticationDTO? current_employee_info = AuthenticateEmployee(employeeLogInDTO);

        if (current_employee_info == null)
        {
            return null;
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); // ?????
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // ?????

        var claims = new[]
        {
            new Claim("EmployeeId", current_employee_info.EmployeeId.ToString()),
            new Claim("InterfaceAccesses", current_employee_info.InterfaceAccesses)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}