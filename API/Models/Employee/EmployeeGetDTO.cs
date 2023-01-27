using API.Models;

public class EmployeeGetDTO
{
    public Guid EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public int Password { get; set; }

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public DateOnly PassportIssueDate { get; set; } 

    public DateOnly StartOfTotalSeniority { get; set; } 

    public DateOnly StartOfLuchSeniority { get; set; }

    public DateOnly? DateOfTermination { get; set; }

    public PositionGetDTO Position { get; set; } = null!;

    public string? Link { get; set; }

    public List<StockDTO>? Stocks { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public decimal Salary { get; set; }

    public decimal PercentageOfSalaryInAdvance { get; set; }

    public DateOnly? DateOfStartInTheCurrentPosition { get; set; }

    public DateOnly? DateOfStartInTheCurrentStock { get; set; }

    public DateOnly? DateOfStartInTheCurrentLink { get; set; }

    public EmployeeGetDTO()
    {

    }

    public EmployeeGetDTO(Employee currentEmployee, PositionGetDTO currentEmployeePosition, List<StockDTO> stocks)
    {
        Password = currentEmployee.Password;
        EmployeeId = currentEmployee.EmployeeId;
        Name = currentEmployee.Name;
        Surname = currentEmployee.Surname;
        Patronymic = currentEmployee.Patronymic;
        Birthday = currentEmployee.Birthday;
        PassportNumber = currentEmployee.PassportNumber;
        PassportIssuer = currentEmployee.PassportIssuer;
        PassportIssueDate = currentEmployee.PassportIssueDate;
        StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority;
        StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority;
        DateOfTermination = currentEmployee.DateOfTermination;
        Position = currentEmployeePosition;
        Link = currentEmployee.Link;
        Stocks = stocks;
        ForkliftControl = currentEmployee.ForkliftControl;
        RolleyesControl = currentEmployee.RolleyesControl;
        Salary = currentEmployee.Salary;
        PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance;
        DateOfStartInTheCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition;
        DateOfStartInTheCurrentStock = currentEmployee.DateOfStartInTheCurrentStock;
        DateOfStartInTheCurrentLink = currentEmployee.DateOfStartInTheCurrentLink;
    }
}
