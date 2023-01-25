using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("[controller]")]
//[Authorize]
public class ShiftController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;
    private readonly ITokenService _tokenService;

    public ShiftController(AccountingOfWorkingHoursContext context, ITokenService tokenService)
    {
        this._context = context;
        this._tokenService = tokenService;
    }

    [HttpPost]
    [Route("open")]
    public async Task<IActionResult> OpenShift([FromBody] ShiftOpenDTO shiftOpenDTO)
    {
        Employee? currentEmployee = _tokenService.GetCurrentEmployee(User, _context);

        if (currentEmployee is null)
        {
            return BadRequest("Сотрудник не найден");
        }

        string employees = JsonConvert.SerializeObject(shiftOpenDTO.Employees);

        Shift shiftToAdd = new Shift(shiftOpenDTO, currentEmployee, employees);
        await _context.Shifts.AddAsync(shiftToAdd);
        await _context.SaveChangesAsync();

        return Ok(shiftToAdd.ShiftId);
    }

    [HttpPost]
    [Route("close")]
    public async Task<IActionResult> CloseShift([FromBody] ShiftCloseDTO shiftCloseDTO)
    {
        Shift? currentShift = await _context.Shifts.FindAsync(shiftCloseDTO.ShiftId);

        if (currentShift is null)
        {
            return NotFound($"Смена {shiftCloseDTO.ShiftId} не найдена");
        }

        currentShift.ClosingDateAndTime = DateTime.UtcNow;

        ShiftHistory shiftHistoryToAdd = new ShiftHistory  // вынести в конструктор
        {
            ShiftHistoryId = currentShift.ShiftId,
            StockId = currentShift.StockId,
            EmployeeWhoPostedTheShiftId = currentShift.EmployeeWhoPostedTheShiftId,
            DayOrNight = currentShift.DayOrNight,
            OpeningDateAndTime = currentShift.OpeningDateAndTime,
            Employees = currentShift.Employees,
            ClosingDateAndTime = currentShift.ClosingDateAndTime
        };

        await _context.ShiftHistories.AddAsync(shiftHistoryToAdd);

        _context.Shifts.Remove(currentShift);

        List<ShiftInfo> info = new();
        foreach (var kv in shiftCloseDTO.WorkedHours)
        {
            Mark? employeeMark = _context.Marks
                .Where(m => m.EmployeeId == new Guid($"{kv.Key}"))
                .FirstOrDefault();

            if (employeeMark is null)
            {
                return BadRequest($"Сотрудник {kv.Key} не был отмечен");
            }

            info.Add(new ShiftInfo 
            {
                ShiftInfoId = new Guid(),
                ShiftHistoryId = shiftHistoryToAdd.ShiftHistoryId,
                EmployeeId = new Guid($"{kv.Key}"),
                DateAndTimeOfArrival = employeeMark.MarkDate,
                DayOrNight = shiftHistoryToAdd.DayOrNight,
                NumberOfHoursWorked = kv.Value
            });

            _context.Marks.Remove(employeeMark);
        }

        await _context.ShiftInfos.AddRangeAsync(info);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("get/{stockId:int}")]
    public async Task<IActionResult> GetCurrentShift(int stockId)
    {
        Shift? currentShift = _context.Shifts.Where(s => s.StockId == stockId).FirstOrDefault();

        if (currentShift is null)
        {
            return NotFound();
        }

        List<string> employeeIds = JsonConvert.DeserializeObject<List<string>>(currentShift.Employees)!;
        List<ShiftEmployeeInfoDTO> employees = new();
        foreach (string employeeId in employeeIds)
        {
            Employee? currentEmployee = await _context.Employees.FindAsync(new Guid(employeeId));

            if (currentEmployee is null)
            {
                return NotFound($"Сотрудник {employeeId} не найден!");
            }

            employees.Add(new ShiftEmployeeInfoDTO
            {
                EmployeeId = currentEmployee.EmployeeId,
                FullName = $"{currentEmployee.Surname} {currentEmployee.Name} {currentEmployee.Patronymic}"
            }
            );
        }

        return Ok(new ShiftGetCurrentDTO
        {
            ShiftId = currentShift.ShiftId,
            DayOrNight = currentShift.DayOrNight,
            Employees = employees
        });
    }
}