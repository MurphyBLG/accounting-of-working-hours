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

            _context.Employees.Add(new Employee
            {
                EmployeeId = employeeRegistrationDTO.Password,
                Name = employeeRegistrationDTO.Name,
                Surname = employeeRegistrationDTO.Surname,
                Patronymic = employeeRegistrationDTO.Patronymic,
                Birthday = DateOnly.Parse(employeeRegistrationDTO.Birthday),
                PassportNumber = employeeRegistrationDTO.PassportNumber,
                PassportIssuer = employeeRegistrationDTO.PassportIssuer,
                PassportIssueDate = DateOnly.Parse(employeeRegistrationDTO.PassportIssueDate),
                StartOfTotalSeniority = DateOnly.Parse(employeeRegistrationDTO.StartOfTotalSeniority),
                StartOfLuchSeniority = DateOnly.Parse(employeeRegistrationDTO.StartOfLuchSeniority),
                DateOfTermination = (employeeRegistrationDTO.DateOfTermination == null) ?
                    null : DateOnly.Parse(employeeRegistrationDTO.DateOfTermination),
                PositionId = employeeRegistrationDTO.PositionId,
                Salary = currentEmployeePosition.Salary,
                QuarterlyBonus = currentEmployeePosition.QuarterlyBonus,
                PercentageOfSalaryInAdvance = employeeRegistrationDTO.PercentageOfSalaryInAdvance,
                Link = employeeRegistrationDTO.Link,
                Stock = employeeRegistrationDTO.Stock,
                ForkliftControl = employeeRegistrationDTO.ForkliftControl,
                RolleyesControl = employeeRegistrationDTO.RolleyesControl,
                DateOfStartInTheCurrentPosition = DateOnly.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentPosition),
                DateOfStartInTheCurrentStock = DateOnly.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentStock)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        _context.SaveChanges();
        return Ok();
    }

    [HttpPost("{EmployeeId:int}")]
    public IActionResult FireEmployee(int employeeId)
    {
        Employee? currentEmployee = _context.Employees.Find(employeeId);

        if (currentEmployee == null)
        {
            return BadRequest("Такого сотрудника не существует");
        }

        _context.EmployeeHistories.Add(new EmployeeHistory
        {
            EmployeeHistoryId = Guid.NewGuid(),
            EmployeeId = currentEmployee.EmployeeId,
            PositionId = currentEmployee.PositionId,
            Link = currentEmployee.Link,
            Stock = currentEmployee.Stock,
            StartDateOfWorkInCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition,
            EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.Now),
            StartDateOfWorkInTheStock = currentEmployee.DateOfStartInTheCurrentStock,
            EndDateOfWorkInTheStock = DateOnly.FromDateTime(DateTime.Now)
        });

        currentEmployee.DateOfTermination = DateOnly.FromDateTime(DateTime.Now);
        currentEmployee.ForkliftControl = false;
        currentEmployee.RolleyesControl = false;
        currentEmployee.PositionId = _firedPositionId;

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

        PositionGetDTO currentEmployeePosition = new()
        {
            PositionId = currentEmployee.PositionId,
            Name = currentEmployee.Position!.Name,
            Salary = currentEmployee.Position!.Salary,
            QuarterlyBonus = currentEmployee.Position!.QuarterlyBonus,
            InterfaceAccesses = currentEmployee.Position!.InterfaceAccesses
        };

        return Ok(new EmployeeGetDTO
        {
            Password = currentEmployee.EmployeeId,
            Name = currentEmployee.Name,
            Surname = currentEmployee.Surname,
            Patronymic = currentEmployee.Patronymic,
            Birthday = currentEmployee.Birthday.ToString(),
            PassportNumber = currentEmployee.PassportNumber,
            PassportIssuer = currentEmployee.PassportIssuer,
            PassportIssueDate = currentEmployee.PassportIssueDate.ToString(),
            StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority.ToString(),
            StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority.ToString(),
            DateOfTermination = currentEmployee.DateOfTermination.ToString(),
            Position = currentEmployeePosition,
            Link = currentEmployee.Link,
            Stock = currentEmployee.Stock,
            ForkliftControl = currentEmployee.ForkliftControl,
            RolleyesControl = currentEmployee.RolleyesControl,
            Salary = currentEmployee.Salary,
            PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance,
            DateOfStartInTheCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition.ToString(),
            DateOfStartInTheCurrentStock = currentEmployee.DateOfStartInTheCurrentStock.ToString()
        });
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
}