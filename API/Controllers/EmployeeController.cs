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

            _context.Employees.Add(new Employee // сделать конструктор
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
                DateOfStartInTheCurrentLink = (employeeRegistrationDTO.Link == null) ? null : DateOnly.FromDateTime(DateTime.UtcNow),
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

    [HttpGet("{EmployeeId:int}")] // Change employeeGetDTO
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

        return Ok(new EmployeeGetDTO // Сделать конструктор
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
            DateOfStartInTheCurrentStock = currentEmployee.DateOfStartInTheCurrentStock.ToString(),
            DateOfStartInTheCurrentLink = currentEmployee.DateOfStartInTheCurrentLink.ToString()
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

    [HttpPut("{EmployeeId:int}")] // Проверить
    public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeUpdateDTO employeeUpdateDTO)
    {
        Employee? currentEmployee = _context.Employees.Find(employeeId);

        if (currentEmployee == null)
        {
            return BadRequest("Сотрудник не найден");
        }

        EmployeeHistory employeeHistory = new() // Добавить конструктор в класс
        {
            EmployeeHistoryId = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = currentEmployee.Name,
            Surname = currentEmployee.Surname,
            Patronymic = currentEmployee.Patronymic,
            Birthday = currentEmployee.Birthday,
            PassportNumber = currentEmployee.PassportNumber,
            PassportIssuer = currentEmployee.PassportIssuer,
            PassportIssueDate = currentEmployee.PassportIssueDate,
            StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority,
            StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority,
            DateOfTermination = currentEmployee.DateOfTermination,
            PositionId = currentEmployee.PositionId,
            StartDateOfWorkInCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition,
            Salary = currentEmployee.Salary,
            QuarterlyBonus = currentEmployee.QuarterlyBonus,
            PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance,
            Link = currentEmployee.Link,
            StartDateOfWorkInCurrentLink = currentEmployee.DateOfStartInTheCurrentLink,
            Stock = currentEmployee.Stock,
            StartDateOfWorkIncurrentStock = currentEmployee.DateOfStartInTheCurrentStock,
            ForkliftControl = currentEmployee.ForkliftControl,
            RolleyesControl = currentEmployee.RolleyesControl,
            DateOfCreation = DateTime.UtcNow
        };

        currentEmployee.Name = employeeUpdateDTO.Name;
        currentEmployee.Surname = employeeUpdateDTO.Surname;
        currentEmployee.Patronymic = employeeUpdateDTO.Patronymic;
        currentEmployee.Birthday = DateOnly.Parse(employeeUpdateDTO.Birthday);
        currentEmployee.PassportNumber = employeeUpdateDTO.PassportNumber;
        currentEmployee.PassportIssuer = employeeUpdateDTO.PassportIssuer;
        currentEmployee.PassportIssueDate = DateOnly.Parse(employeeUpdateDTO.PassportIssueDate);
        currentEmployee.StartOfTotalSeniority = DateOnly.Parse(employeeUpdateDTO.StartOfTotalSeniority);
        currentEmployee.StartOfLuchSeniority = DateOnly.Parse(employeeUpdateDTO.StartOfLuchSeniority);

        if (employeeUpdateDTO.PositionId == _firedPositionId && currentEmployee.PositionId != employeeUpdateDTO.PositionId)
            currentEmployee.DateOfTermination = employeeUpdateDTO.DateOfTermination == null ? DateOnly.FromDateTime(DateTime.UtcNow) : DateOnly.FromDateTime(DateTime.Parse(employeeUpdateDTO.DateOfTermination));

        if (employeeUpdateDTO.PositionId != _firedPositionId)
            currentEmployee.DateOfTermination = null;

        if (currentEmployee.PositionId != employeeUpdateDTO.PositionId)
        {
            currentEmployee.PositionId = employeeUpdateDTO.PositionId;
            currentEmployee.DateOfStartInTheCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);

            employeeHistory.EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
        }

        if (currentEmployee.PositionId == _firedPositionId)
        {
            currentEmployee.Link = null;
            currentEmployee.DateOfStartInTheCurrentLink = null;

            employeeHistory.EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.Stock = null;
            currentEmployee.DateOfStartInTheCurrentStock = null;

            employeeHistory.EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);

            currentEmployee.Salary = 0;
            currentEmployee.QuarterlyBonus = 0;
            currentEmployee.PercentageOfSalaryInAdvance = 0;
            currentEmployee.ForkliftControl = false;
            currentEmployee.RolleyesControl = false;
        }
        else
        {
            if (currentEmployee.Link != employeeUpdateDTO.Link)
            {
                currentEmployee.Link = employeeUpdateDTO.Link;
                currentEmployee.DateOfStartInTheCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);

                employeeHistory.EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);
            }

            if (currentEmployee.Stock != employeeUpdateDTO.Stock)
            {
                currentEmployee.Stock = employeeUpdateDTO.Stock;
                currentEmployee.DateOfStartInTheCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);

                employeeHistory.EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
            }

            currentEmployee.Salary = employeeUpdateDTO.Salary;
            currentEmployee.QuarterlyBonus = employeeUpdateDTO.QuarterlyBonus;
            currentEmployee.PercentageOfSalaryInAdvance = employeeUpdateDTO.PercentageOfSalaryInAdvance;
            currentEmployee.ForkliftControl = employeeUpdateDTO.ForkliftControl;
            currentEmployee.RolleyesControl = employeeUpdateDTO.RolleyesControl;
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