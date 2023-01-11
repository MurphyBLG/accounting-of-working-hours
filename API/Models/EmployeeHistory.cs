namespace API.Models;

public partial class EmployeeHistory
{
    public Guid? EmployeeHistoryId { get; set; }

    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public DateOnly PassportIssueDate { get; set; }

    public DateOnly StartOfTotalSeniority { get; set; }

    public DateOnly StartOfLuchSeniority { get; set; }

    public DateOnly? DateOfTermination { get; set; }

    public Guid? PositionId { get; set; }

    public DateOnly? StartDateOfWorkInCurrentPosition { get; set; }

    public DateOnly? EndDateOfWorkInCurrentPosition { get; set; }

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }

    public string? Link { get; set; }

    public DateOnly? StartDateOfWorkInCurrentLink { get; set; }

    public DateOnly? EndDateOfWorkInCurrentLink { get; set; }

    public string? Stock { get; set; }

    public DateOnly? StartDateOfWorkIncurrentStock { get; set; }

    public DateOnly? EndDateOfWorkInCurrentStock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public DateTime DateOfCreation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }
}
