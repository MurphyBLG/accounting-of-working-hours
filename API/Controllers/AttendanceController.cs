using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[Route("[controller]")]
//[Authorize]
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
        List<AttendaceEmployee> employeesOfCurretntStock = GetEmployeesOfCurrentStock(attendancePeriodDTO.StockId);

        List<AttendanceShiftInfoDTO> shiftsOfCurrentMonth = GetShiftsOfMonth(attendancePeriodDTO.Month);

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

    private List<AttendaceEmployee> GetEmployeesOfCurrentStock(int stockId)
    {
        IEnumerable<AttendaceEmployee> result = _context.Employees.Include(e => e.Position)
            .Where(ContainsStock(stockId))
            .Select(employee => new AttendaceEmployee
            {
                EmployeeId = employee.EmployeeId,
                FullName = $"{employee.Surname} {employee.Name} {employee.Patronymic}",
                PositionName = employee.Position!.Name
            });

        return result.ToList();
    }

    private static Func<Employee, bool> ContainsStock(int stockId)
    {
        return e =>
        {
            HashSet<int> stocks = JsonConvert.DeserializeObject<HashSet<int>>(e.Stocks!)!;

            return stocks.Contains(stockId);
        };
    }

    private List<AttendanceShiftInfoDTO> GetShiftsOfMonth(int month)
    {
        IEnumerable<AttendanceShiftInfoDTO> result = _context.ShiftInfos
            .Where(s => s.DateAndTimeOfArrival.Month == month)
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