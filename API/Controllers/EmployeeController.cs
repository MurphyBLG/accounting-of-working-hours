using API.Models;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class EmployeeController : Controller
{
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
}