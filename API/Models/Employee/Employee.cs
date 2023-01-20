namespace API.Models;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public int Password { get; set; }

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

    public DateOnly? DateOfStartInTheCurrentPosition { get; set; }

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }

    public string? Link { get; set; }

    public DateOnly? DateOfStartInTheCurrentLink { get; set; }

    public string? Stocks { get; set; }

    public DateOnly? DateOfStartInTheCurrentStock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    public virtual Position? Position { get; set; }

    public Employee(EmployeeRegistrationDTO employeeRegistrationDTO, Position employeePosition)
    {
        EmployeeId = Guid.NewGuid();
        Password = employeeRegistrationDTO.Password;
        Name = employeeRegistrationDTO.Name;
        Surname = employeeRegistrationDTO.Surname;
        Patronymic = employeeRegistrationDTO.Patronymic;
        Birthday = DateOnly.Parse(employeeRegistrationDTO.Birthday);
        PassportNumber = employeeRegistrationDTO.PassportNumber;
        PassportIssuer = employeeRegistrationDTO.PassportIssuer;
        PassportIssueDate = DateOnly.Parse(employeeRegistrationDTO.PassportIssueDate);
        StartOfTotalSeniority = DateOnly.Parse(employeeRegistrationDTO.StartOfTotalSeniority);
        StartOfLuchSeniority = DateOnly.Parse(employeeRegistrationDTO.StartOfLuchSeniority);
        DateOfTermination = (employeeRegistrationDTO.DateOfTermination == null) ?
            null : DateOnly.Parse(employeeRegistrationDTO.DateOfTermination);
        PositionId = employeeRegistrationDTO.PositionId;
        Salary = employeePosition.Salary;
        QuarterlyBonus = employeePosition.QuarterlyBonus;
        PercentageOfSalaryInAdvance = employeeRegistrationDTO.PercentageOfSalaryInAdvance;
        Link = employeeRegistrationDTO.Link;
        DateOfStartInTheCurrentLink = (employeeRegistrationDTO.Link == null) ? null : DateOnly.FromDateTime(DateTime.UtcNow); // А если задали?
        Stocks = employeeRegistrationDTO.Stock;
        ForkliftControl = employeeRegistrationDTO.ForkliftControl;
        RolleyesControl = employeeRegistrationDTO.RolleyesControl;
        DateOfStartInTheCurrentPosition = DateOnly.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentPosition);
        DateOfStartInTheCurrentStock = (employeeRegistrationDTO.DateOfStartInTheCurrentStock == null) ? null : DateOnly.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentStock); // А если не задано?
    }

    public Employee()
    {
    }
}
