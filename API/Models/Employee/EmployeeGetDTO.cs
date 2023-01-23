using API.Models;

public class EmployeeGetDTO
{
    public Guid EmployeeId { get; set; }

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

    public Dictionary<string, int>? Stocks { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public decimal Salary { get; set; }

    public decimal PercentageOfSalaryInAdvance { get; set; }

    public string? DateOfStartInTheCurrentPosition { get; set; }

    public string? DateOfStartInTheCurrentStock { get; set; }

    public string? DateOfStartInTheCurrentLink { get; set; }

    public EmployeeGetDTO()
    {

    }

    public EmployeeGetDTO(Employee currentEmployee, PositionGetDTO currentEmployeePosition, Dictionary<string, int> stocks)
    {
        EmployeeId = currentEmployee.EmployeeId;
        Name = currentEmployee.Name;
        Surname = currentEmployee.Surname;
        Patronymic = currentEmployee.Patronymic;
        Birthday = currentEmployee.Birthday.ToString();
        PassportNumber = currentEmployee.PassportNumber;
        PassportIssuer = currentEmployee.PassportIssuer;
        PassportIssueDate = currentEmployee.PassportIssueDate.ToString();
        StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority.ToString();
        StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority.ToString();
        DateOfTermination = currentEmployee.DateOfTermination.ToString();
        Position = currentEmployeePosition;
        Link = currentEmployee.Link;
        Stocks = stocks;
        ForkliftControl = currentEmployee.ForkliftControl;
        RolleyesControl = currentEmployee.RolleyesControl;
        Salary = currentEmployee.Salary;
        PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance;
        DateOfStartInTheCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition.ToString();
        DateOfStartInTheCurrentStock = currentEmployee.DateOfStartInTheCurrentStock.ToString();
        DateOfStartInTheCurrentLink = currentEmployee.DateOfStartInTheCurrentLink.ToString();
    }
}
