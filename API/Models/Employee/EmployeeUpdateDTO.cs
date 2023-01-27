namespace API.Models;

public class EmployeeUpdateDTO
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int Password { get; set; }

    public string Patronymic { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public DateTime PassportIssueDate { get; set; }

    public DateTime StartOfTotalSeniority { get; set; }

    public DateTime StartOfLuchSeniority { get; set; }

    public DateTime? DateOfTermination { get; set; }

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public string? Stocks { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public decimal Salary { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }
    
    public decimal QuarterlyBonus { get; set; }

    public DateTime? DateOfStartInTheCurrentPosition { get; set; }

    public DateTime? DateOfStartInTheCurrentStock { get; set; }

    public DateTime? DateOfStartInTheCurrentLink { get; set; }
}
