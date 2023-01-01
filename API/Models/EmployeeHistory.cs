namespace API.Models;

public partial class EmployeeHistory
{
    public Guid? EmployeeHistoryId { get; set; }

    public int EmployeeId { get; set; }

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public string? Stock { get; set; }

    public DateOnly StartDateOfWorkInCurrentPosition { get; set; }

    public DateOnly EndDateOfWorkInCurrentPosition { get; set; }

    public DateOnly StartDateOfWorkInTheStock { get; set; }

    public DateOnly EndDateOfWorkInTheStock { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }
}
