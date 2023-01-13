using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[Authorize]
public class EmployeeController : Controller
{
    private readonly Guid _firedPositionId = new("3d7e357d-ca7f-44c1-bbfb-a6250c5b7239");
    private readonly AccountingOfWorkingHoursContext _context;

    public EmployeeController(AccountingOfWorkingHoursContext context)
    {
        this._context = context;
    }

    [HttpPost]
    public IActionResult RegisterEmployee([FromBody] EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        try
        {
            Position? currentEmployeePosition = _context.Positions.Find(employeeRegistrationDTO.PositionId);

            if (currentEmployeePosition == null)
            {
                return BadRequest("Такой должности не существует");
            }

            _context.Employees.Add(new Employee(employeeRegistrationDTO, currentEmployeePosition));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("{EmployeeId:int}")]
    public async Task<IActionResult> GetEmployee(int employeeId)
    {
        Employee? currentEmployee = await _context.Employees.FindAsync(employeeId);

        if (currentEmployee == null)
        {
            return BadRequest("Такого сотрудника не существует");
        }

        await _context.Entry(currentEmployee).Reference(e => e.Position).LoadAsync();

        PositionGetDTO currentEmployeePosition = new(currentEmployee);

        return Ok(new EmployeeGetDTO(currentEmployee, currentEmployeePosition));
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        IEnumerable<EmployeeGetAllDTO>? result = from e in _context.Employees
                                                 select new EmployeeGetAllDTO
                                                 {
                                                     Password = e.EmployeeId,
                                                     Name = e.Name,
                                                     Surname = e.Surname,
                                                     Patronymic = e.Patronymic
                                                 };

        return Ok(result);
    }

    [HttpPut("{EmployeeId:int}")]
    public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeUpdateDTO employeeUpdateDTO)
    {
        Employee? currentEmployee = _context.Employees.Find(employeeId);

        if (currentEmployee == null)
        {
            return BadRequest("Сотрудник не найден");
        }

        EmployeeHistory employeeHistory = new(employeeId, currentEmployee);

        currentEmployee.Name = employeeUpdateDTO.Name;
        currentEmployee.Surname = employeeUpdateDTO.Surname;
        currentEmployee.Patronymic = employeeUpdateDTO.Patronymic;
        currentEmployee.Birthday = DateOnly.Parse(employeeUpdateDTO.Birthday);
        currentEmployee.PassportNumber = employeeUpdateDTO.PassportNumber;
        currentEmployee.PassportIssuer = employeeUpdateDTO.PassportIssuer;
        currentEmployee.PassportIssueDate = DateOnly.Parse(employeeUpdateDTO.PassportIssueDate);
        currentEmployee.StartOfTotalSeniority = DateOnly.Parse(employeeUpdateDTO.StartOfTotalSeniority);
        currentEmployee.StartOfLuchSeniority = DateOnly.Parse(employeeUpdateDTO.StartOfLuchSeniority);

        if (currentEmployee.Stock != employeeUpdateDTO.Stock)
        {
            employeeHistory.EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.DateOfStartInTheCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
            currentEmployee.Stock = employeeUpdateDTO.Stock;
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

            if (employeeUpdateDTO.PositionId == _firedPositionId)
            {
                currentEmployee.DateOfTermination = employeeUpdateDTO.DateOfTermination == null ? DateOnly.FromDateTime(DateTime.UtcNow) : DateOnly.FromDateTime(DateTime.Parse(employeeUpdateDTO.DateOfTermination));
                currentEmployee.DateOfStartInTheCurrentStock = null;
                currentEmployee.Stock = null;
                currentEmployee.DateOfStartInTheCurrentLink = null;
                currentEmployee.Link = null;
            }
        }

        try
        {
            _context.EmployeeHistories.Add(employeeHistory);

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}