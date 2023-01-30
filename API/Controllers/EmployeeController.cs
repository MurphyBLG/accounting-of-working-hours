using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[Authorize]
public class EmployeeController : Controller
{
    private readonly Guid _firedPositionId = new("9ad29fb2-f9c4-4e4d-9155-12af0227ea67");
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly IIdstoIdsPlusDataService _idstoIdsPlusDataService;

    public EmployeeController(AccountingOfWorkingHoursContext context, IIdstoIdsPlusDataService idstoIdsPlusDataService)
    {
        this._context = context;
        this._idstoIdsPlusDataService = idstoIdsPlusDataService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        try
        {
            Position? currentEmployeePosition = await _context.Positions.FindAsync(employeeRegistrationDTO.PositionId);

            if (currentEmployeePosition == null)
            {
                return BadRequest("Такой должности не существует");
            }

            Employee employeeToAdd = new Employee(employeeRegistrationDTO, currentEmployeePosition);

            await _context.Employees.AddAsync(employeeToAdd);

            await _context.SaveChangesAsync();

            return Ok(employeeToAdd);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{EmployeeId}")]
    public async Task<IActionResult> GetEmployee(string employeeId)
    {
        Employee? currentEmployee = await _context.Employees.FindAsync(new Guid(employeeId));

        if (currentEmployee == null)
        {
            return BadRequest("Такого сотрудника не существует");
        }

        await _context.Entry(currentEmployee).Reference(e => e.Position).LoadAsync();

        PositionGetDTO currentEmployeePosition = new(currentEmployee);

        return Ok(new EmployeeGetDTO(currentEmployee,
            currentEmployeePosition,
            _idstoIdsPlusDataService.StocksToList(currentEmployee, _context)));
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        List<Employee>? employees = _context.Employees.ToList();

        IEnumerable<EmployeeGetAllDTO> result = employees.Select(e => new EmployeeGetAllDTO
        {
            EmployeeId = e.EmployeeId,
            Name = e.Name,
            Surname = e.Surname,
            Patronymic = e.Patronymic,
            Stocks = _idstoIdsPlusDataService.StocksToList(e, _context),
            Link = e.Link,
        });

        return Ok(result);
    }

    [HttpPut("{EmployeeId}")]
    public async Task<IActionResult> UpdateEmployee(string employeeId, [FromBody] EmployeeUpdateDTO employeeUpdateDTO)
    {
        Employee? currentEmployee = await _context.Employees.FindAsync(new Guid(employeeId));

        if (currentEmployee == null)
        {
            return BadRequest("Сотрудник не найден");
        }

        EmployeeHistory employeeHistory = new(employeeId, currentEmployee);

        currentEmployee.Name = employeeUpdateDTO.Name;
        currentEmployee.Password = employeeUpdateDTO.Password;
        currentEmployee.Surname = employeeUpdateDTO.Surname;
        currentEmployee.Patronymic = employeeUpdateDTO.Patronymic;
        currentEmployee.Birthday = DateOnly.FromDateTime(employeeUpdateDTO.Birthday);
        currentEmployee.PassportNumber = employeeUpdateDTO.PassportNumber;
        currentEmployee.PassportIssuer = employeeUpdateDTO.PassportIssuer;
        currentEmployee.PassportIssueDate = DateOnly.FromDateTime(employeeUpdateDTO.PassportIssueDate);
        currentEmployee.StartOfTotalSeniority = DateOnly.FromDateTime(employeeUpdateDTO.StartOfTotalSeniority);
        currentEmployee.StartOfLuchSeniority = DateOnly.FromDateTime(employeeUpdateDTO.StartOfLuchSeniority);
        currentEmployee.ForkliftControl = employeeUpdateDTO.ForkliftControl;
        currentEmployee.RolleyesControl = employeeUpdateDTO.RolleyesControl;
        currentEmployee.Salary = employeeUpdateDTO.Salary;
        currentEmployee.PercentageOfSalaryInAdvance = employeeUpdateDTO.PercentageOfSalaryInAdvance;

        if (currentEmployee.Stocks != employeeUpdateDTO.Stocks)
        {
            employeeHistory.EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.DateOfStartInTheCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
            currentEmployee.Stocks = employeeUpdateDTO.Stocks;
        }

        if (currentEmployee.Link != employeeUpdateDTO.Link)
        {
            employeeHistory.EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.DateOfStartInTheCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);
            currentEmployee.Link = employeeUpdateDTO.Link;
        }

        if (currentEmployee.PositionId != employeeUpdateDTO.PositionId)
        {
            employeeHistory.EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.DateOfStartInTheCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
            currentEmployee.PositionId = employeeUpdateDTO.PositionId;
            currentEmployee.DateOfTermination = null;
        }

        try
        {
            await _context.EmployeeHistories.AddAsync(employeeHistory);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        await _context.Entry(currentEmployee).Reference(e => e.Position).LoadAsync();

        return Ok(currentEmployee);
    }

    [HttpDelete("{EmployeeId}")]
    public async Task<IActionResult> FireEmployee(string employeeId, [FromBody] EmployeeFireDTO employeeFireDTO)
    {
        Employee? currentEmployee = await _context.Employees.FindAsync(new Guid(employeeId));

        if (currentEmployee == null)
        {
            return BadRequest("Сотрудник не найден");
        }

        EmployeeHistory employeeHistory = new(employeeId, currentEmployee);

        employeeHistory.EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
        employeeHistory.EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);
        employeeHistory.EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);

        currentEmployee.DateOfStartInTheCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
        currentEmployee.PositionId = _firedPositionId;
        currentEmployee.DateOfTermination = DateOnly.FromDateTime(employeeFireDTO.DateOfTermination);
        currentEmployee.DateOfStartInTheCurrentStock = null;
        currentEmployee.Stocks = "[]";
        currentEmployee.DateOfStartInTheCurrentLink = null;
        currentEmployee.Link = null;
        currentEmployee.ForkliftControl = false;
        currentEmployee.RolleyesControl = false;

        try
        {
            await _context.EmployeeHistories.AddAsync(employeeHistory);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}