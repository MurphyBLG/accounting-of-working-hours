public class AttendanceShiftInfoDTO
{
    public Guid ShiftId { get; set; }

    public Guid EmployeeId { get; set; }

    public int Day { get; set; }  

    public string DayOrNight { get; set; } = null!;

    public int WorkedHours { get; set; }

    public decimal? Penalty { get; set; }

    public string? PenaltyComment { get; set; }

    public decimal? Send { get; set; }

    public string? SendComment { get; set; }
}