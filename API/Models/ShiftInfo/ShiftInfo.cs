using API.Models;

public class ShiftInfo 
{
    public Guid ShiftInfoId { get; set; }

    public Guid? ShiftHistoryId { get; set; }

    public Guid EmployeeId { get; set; }

    public DateTime DateAndTimeOfArrival { get; set; }

    public int NumberOfHoursWorked { get; set; }

    public decimal? Penalty { get; set; }

    public string? PenaltyComment { get; set; }

    public decimal? Send { get; set; }

    public string? SendComment { get; set; }


    public virtual ShiftHistory? ShiftHistory { get; set; }

    public virtual Employee? Employee { get; set; }
}