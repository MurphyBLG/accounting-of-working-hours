using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class LogInController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;

    public LogInController(AccountingOfWorkingHoursContext context, IConfiguration config, ITokenService tokenService)
    {
        this._context = context;
        this._config = config;
        this._tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult AuthorizeEmployee([FromBody] EmployeeLogInDTO employeeLogInDTO)
    {
        Employee? currentEmployee = this.AuthenticateEmployee(employeeLogInDTO);

        if (currentEmployee == null)
        {
            return BadRequest("Authentication error: There is no such user");
        }

        string token = this._tokenService.BuildToken(currentEmployee,
            _config["JWT:Key"]!,
            _config["Jwt:Issuer"]!,
            _config["Jwt:Audience"]!);

        return Ok(new InterfaceAccessesDTO
        {
            Accesses = JsonSerializer.Deserialize<InterfaceAccesses>(currentEmployee.Position!.InterfaceAccesses)!,
            Token = token
        });
    }

    private Employee? AuthenticateEmployee(EmployeeLogInDTO employeeLogInDTO)
    {
        Employee? currentEmployee = _context.Employees
            .Find(employeeLogInDTO.Password);

        if (currentEmployee == null)
        {
            return null;
        }

        _context.Entry(currentEmployee).Reference(e => e.Position).Load();

        return currentEmployee;
    }
}