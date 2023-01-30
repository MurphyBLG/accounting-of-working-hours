using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[Route("[controller]")]
[Authorize]
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
            ClosingDateAndTime = currentShift.ClosingDateAndTime,
            LastUpdate = currentShift.LastUpdate
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
                return BadRequest($"Сотрудник {kv.Key} не был отмечен"); // нормально присоединить ФИО
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

        List<Mark> marksToRemove = _context.Marks.Where(m => m.StockId == shiftHistoryToAdd.StockId)
            .Include(m => m.Employee).ToList();

        List<ShiftUntrackedEmployeeDTO> untrackedEmployees = new();
        foreach (var mark in marksToRemove)
        {
            if (mark.Employee is not null)
            {
                untrackedEmployees.Add(new ShiftUntrackedEmployeeDTO
                {
                    EmployeeId = mark.EmployeeId,
                    FullName = $"{mark.Employee.Surname} {mark.Employee.Name} {mark.Employee.Patronymic}"
                });
            }
        }

        if (untrackedEmployees.Count() != 0)
        {
            return BadRequest($"Следующие сотрудники были отмечены, но не были назначены на смену: {JsonConvert.SerializeObject(untrackedEmployees)}");
        }

        _context.Marks.RemoveRange(marksToRemove);

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

    [HttpPut("{shiftId:Guid}")]
    public async Task<IActionResult> UpdateCurrentShift(Guid shiftId, [FromBody] ShiftUpdateDTO shiftUpdateDTO)
    {
        Shift? currentShift = await _context.Shifts.FindAsync(shiftId);

        if (currentShift is null)
        {
            return NotFound($"Shift {shiftId} not found");
        }

        currentShift.Employees = JsonConvert.SerializeObject(shiftUpdateDTO.Employees);
        currentShift.DayOrNight = shiftUpdateDTO.DayOrNight;
        currentShift.LastUpdate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("{shiftInfoId:Guid}")]
    public async Task<IActionResult> UpdateShiftInfo(Guid shiftInfoId, [FromBody] ShiftInfoUpdateDTO shiftInfoUpdateDTO)
    {
        ShiftInfo? currentShiftInfo = await _context.ShiftInfos.FindAsync(shiftInfoId);

        if (currentShiftInfo is null)
        {
            return NotFound($"Смена {shiftInfoId} не найдена");
        }

        currentShiftInfo.Penalty = shiftInfoUpdateDTO.Penalty;
        currentShiftInfo.PenaltyComment = shiftInfoUpdateDTO.PenaltyComment;

        currentShiftInfo.Send = shiftInfoUpdateDTO.Send;
        currentShiftInfo.SendComment = shiftInfoUpdateDTO.SendComment;

        await _context.SaveChangesAsync();

        return Ok();
    }
}