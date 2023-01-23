using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("[controller]")]
[Authorize]
public class LogInController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;
    private readonly IDictionarizatorService _dictionarizator;

    public LogInController(AccountingOfWorkingHoursContext context, IConfiguration config, ITokenService tokenService, IDictionarizatorService dictionarizator)
    {
        this._context = context;
        this._config = config;
        this._tokenService = tokenService;
        this._dictionarizator = dictionarizator;
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

        Dictionary <string, int> stocksWithNames = _dictionarizator.DictionarizeStocks(currentEmployee, _context);

        return Ok(new InterfaceAccessesDTO
        {
            Accesses = System.Text.Json.JsonSerializer.Deserialize<InterfaceAccesses>(currentEmployee.Position!.InterfaceAccesses)!,
            Token = token,
            Stocks = stocksWithNames
        });
    }

    private Employee? AuthenticateEmployee(EmployeeLogInDTO employeeLogInDTO)
    {
        Employee? currentEmployee = _context.Employees
            .Where(e => e.Password == employeeLogInDTO.Password)
            .FirstOrDefault();

        if (currentEmployee == null)
        {
            return null;
        }

        _context.Entry(currentEmployee).Reference(e => e.Position).Load();

        return currentEmployee;
    }
}