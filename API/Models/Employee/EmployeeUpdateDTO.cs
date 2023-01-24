namespace API.Models;

public class EmployeeUpdateDTO
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int Password { get; set; }

    public string Patronymic { get; set; } = null!;

    public string Birthday { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public string PassportIssueDate { get; set; } = null!;

    public string StartOfTotalSeniority { get; set; } = null!;

    public string StartOfLuchSeniority { get; set; } = null!;

    public string? DateOfTermination { get; set; }

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public string? Stocks { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public decimal Salary { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }
    
    public decimal QuarterlyBonus { get; set; }

    public string? DateOfStartInTheCurrentPosition { get; set; }

    public string? DateOfStartInTheCurrentStock { get; set; }

    public string? DateOfStartInTheCurrentLink { get; set; }
}
