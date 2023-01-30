using System.Security.Claims;
using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/[controller]")]
[Authorize]
public class LogInController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;
    private readonly IIdstoIdsPlusDataService _idstoIdsPlusDataService;
    private readonly Guid _moverId = new Guid("d370e736-8466-437c-a739-121bf353c01a");

    public LogInController(AccountingOfWorkingHoursContext context, IConfiguration config, ITokenService tokenService, IIdstoIdsPlusDataService idstoIdsPlusDataService)
    {
        this._context = context;
        this._config = config;
        this._tokenService = tokenService;
        this._idstoIdsPlusDataService = idstoIdsPlusDataService;
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

        RefreshToken refreshToken = _tokenService.GenerateRefreshToken();

        currentEmployee.RefreshToken = refreshToken.Token;
        currentEmployee.RefreshTokenExpires = refreshToken.Expires;

        SetRefreshToken(refreshToken);

        if (currentEmployee.PositionId == _moverId)
        {
            _context.Marks.Add(new Mark
            {
                MarkId = new Guid(),
                EmployeeId = currentEmployee.EmployeeId,
                StockId = JsonConvert.DeserializeObject<List<int>>(currentEmployee.Stocks!)![0],
                MarkDate = DateTime.UtcNow
            });
        }

        _context.SaveChanges();

        List<StockDTO> stocksWithNames = _idstoIdsPlusDataService.StocksToList(currentEmployee, _context);

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

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string token)
    {
        string? refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken is null)
        {
            return Unauthorized("Token not found");
        }

        var oldClaims = _tokenService.GetPrincipalFromExpiredToken(token, _config);

        Guid currentEmployeeId = new(oldClaims!.FindFirstValue("EmployeeId")!);
        Employee? currentEmployee = await _context.Employees.FindAsync(currentEmployeeId);

        if (currentEmployee is null)
        {
            return NotFound("Employee is not found");
        }

        if (currentEmployee.RefreshToken != refreshToken)
        {
            return Unauthorized("Invalid refresh token");
        }

        if (currentEmployee.RefreshTokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired");
        }

        string newToken = _tokenService.BuildToken(currentEmployee,
            _config["JWT:Key"]!,
            _config["Jwt:Issuer"]!,
            _config["Jwt:Audience"]!);

        return Ok(newToken);
    }

    private void SetRefreshToken(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}