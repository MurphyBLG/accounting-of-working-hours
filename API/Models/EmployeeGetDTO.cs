using API.Models;

public class EmployeeGetDTO
{
    public int Password { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Birthday { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public string PassportIssueDate { get; set; } = null!;

    public string StartOfTotalSeniority { get; set; } = null!;

    public string StartOfLuchSeniority { get; set; } = null!;

    public string? DateOfTermination { get; set; }

    public PositionGetDTO Position { get; set; } = null!;

    public string? Link { get; set; }

    public string? Stock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public decimal Salary { get; set; }

    public decimal PercentageOfSalaryInAdvance { get; set; }

    public string? DateOfStartInTheCurrentPosition { get; set; } 

    public string? DateOfStartInTheCurrentStock { get; set; } 

    public string? DateOfStartInTheCurrentLink { get; set; } 
}
