namespace API.Models;

public class EmployeeRegistrationDTO
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

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public string? Stock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }

    public string DateOfStartInTheCurrentPosition { get; set; } = null!;

    public string? DateOfStartInTheCurrentStock { get; set; }

    public string? DateOfStartInTheCurrentLink { get; set; }
}
