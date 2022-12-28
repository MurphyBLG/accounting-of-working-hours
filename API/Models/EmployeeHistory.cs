namespace API.Models;

public partial class EmployeeHistory
{
    public Guid? EmployeeHistoryId { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public DateOnly StartDateOfWorkInCurrentPosition { get; set; }

    public DateOnly EndDateOfWorkInCurrentPosition { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }
}
