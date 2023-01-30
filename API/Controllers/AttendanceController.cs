using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[Route("[controller]")]
[Authorize]
public class AttendanceController : Controller
{
    private readonly AccountingOfWorkingHoursContext _context;

    public AttendanceController(AccountingOfWorkingHoursContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public IActionResult GetAttendance([FromBody] AttendancePeriodDTO attendancePeriodDTO)
    {
        List<AttendaceEmployee> employeesOfCurretntStock = GetEmployeesOfCurrentStock(attendancePeriodDTO.StockId, attendancePeriodDTO.Month, attendancePeriodDTO.Year);

        List<AttendanceShiftInfoDTO> shiftsOfCurrentMonth = GetShiftsOfMonth(attendancePeriodDTO.Month, attendancePeriodDTO.Year);

        List<AttendanceDTO> response = new();
        foreach (AttendaceEmployee employee in employeesOfCurretntStock)
        {
            AttendanceDTO attendance = new();
            attendance.Employee = employee;
            attendance.Shifts = shiftsOfCurrentMonth.Where(s => s.EmployeeId == employee.EmployeeId).ToList();

            if (attendance.Shifts.Count() != 0)
            {
                response.Add(attendance);
            }
        }

        return Ok(response);
    }

    private List<AttendaceEmployee> GetEmployeesOfCurrentStock(int stockId, int month, int year)
    {
        IEnumerable<AttendaceEmployee> result = _context.ShiftInfos.Include(s => s.ShiftHistory)
            .Where(s => s.ShiftHistory!.StockId == stockId)
            .Where(s => s.DateAndTimeOfArrival!.Month == month)
            .Where(s => s.DateAndTimeOfArrival!.Year == year)
            .Include(s => s.Employee)
                .ThenInclude(e => e!.Position)
                    .Select(shiftInfo => new AttendaceEmployee
                    {
                        EmployeeId = shiftInfo.EmployeeId,
                        FullName = $"{shiftInfo.Employee!.Surname} {shiftInfo.Employee.Name} {shiftInfo.Employee.Patronymic}",
                        PositionName = shiftInfo.Employee.Position!.Name
                    }).Distinct();

        return result.ToList();
    }

    private List<AttendanceShiftInfoDTO> GetShiftsOfMonth(int month, int year)
    {
        IEnumerable<AttendanceShiftInfoDTO> result = _context.ShiftInfos
            .Where(s => s.DateAndTimeOfArrival.Month == month)
            .Where(s => s.DateAndTimeOfArrival.Year == year)
            .Select(shift => new AttendanceShiftInfoDTO
            {
                ShiftId = shift.ShiftInfoId,
                EmployeeId = shift.EmployeeId,
                Day = shift.DateAndTimeOfArrival.Day,
                DayOrNight = shift.DayOrNight,
                WorkedHours = shift.NumberOfHoursWorked,
                Penalty = shift.Penalty,
                PenaltyComment = shift.PenaltyComment,
                Send = shift.Send,
                SendComment = shift.SendComment
            });

        return result.ToList();
    }
}