using API.Models;

public class Shift
{
    public Guid ShiftId { get; set; }

    public int StockId { get; set; }

    public Guid EmployeeWhoPostedTheShiftId { get; set; }

    public string DayOrNight { get; set; } = null!;

    public DateTime? OpeningDateAndTime { get; set; }

    public string Employees { get; set; } = null!;

    public DateTime? ClosingDateAndTime { get; set; }

    public DateTime? LastUpdate { get; set; }


    public virtual Stock? Stock { get; set; }

    public virtual Employee? EmployeeWhoPostedTheShift { get; set; }

    public Shift(ShiftOpenDTO shiftOpenDTO, Employee opener, string employees)
    {
        ShiftId = new Guid();
        StockId = shiftOpenDTO.StockId;
        EmployeeWhoPostedTheShiftId = opener.EmployeeId;
        DayOrNight = shiftOpenDTO.DayOrNight;
        OpeningDateAndTime = DateTime.UtcNow;
        Employees = employees;
        LastUpdate = DateTime.UtcNow;
    }

    public Shift()
    {}
}