using Newtonsoft.Json;

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

    [JsonIgnore]
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

    [JsonIgnore]
    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    [JsonIgnore]
    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();

    [JsonIgnore]
    public virtual ICollection<Mark> Marks { get; } = new List<Mark>();

    [JsonIgnore]
    public virtual ICollection<WorkPlan> WorkPlans { get; } = new List<WorkPlan>();

    [JsonIgnore]
    public virtual ICollection<ShiftInfo> ShiftInfos { get; } = new List<ShiftInfo>();

    [JsonIgnore]
    public virtual ICollection<ShiftHistory> ShiftHistories { get; } = new List<ShiftHistory>();

    public virtual Position? Position { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonIgnore]
    public DateTime RefreshTokenExpires { get; set; }

    public Employee(EmployeeRegistrationDTO employeeRegistrationDTO, Position employeePosition)
    {
        EmployeeId = Guid.NewGuid();
        Password = employeeRegistrationDTO.Password;
        Name = employeeRegistrationDTO.Name;
        Surname = employeeRegistrationDTO.Surname;
        Patronymic = employeeRegistrationDTO.Patronymic;
        Birthday = DateOnly.FromDateTime(employeeRegistrationDTO.Birthday);
        PassportNumber = employeeRegistrationDTO.PassportNumber;
        PassportIssuer = employeeRegistrationDTO.PassportIssuer;
        PassportIssueDate = DateOnly.FromDateTime(employeeRegistrationDTO.PassportIssueDate);
        StartOfTotalSeniority = DateOnly.FromDateTime(employeeRegistrationDTO.StartOfTotalSeniority);
        StartOfLuchSeniority = DateOnly.FromDateTime(employeeRegistrationDTO.StartOfLuchSeniority);
        DateOfTermination = (employeeRegistrationDTO.DateOfTermination == null) ?
            null : DateOnly.FromDateTime(DateTime.Parse(employeeRegistrationDTO.DateOfTermination.ToString()!));
        PositionId = employeeRegistrationDTO.PositionId;
        Salary = employeeRegistrationDTO.Salary;
        QuarterlyBonus = employeePosition.QuarterlyBonus;
        PercentageOfSalaryInAdvance = employeeRegistrationDTO.PercentageOfSalaryInAdvance;
        Link = employeeRegistrationDTO.Link;
        DateOfStartInTheCurrentLink = (employeeRegistrationDTO.Link == null) ? null : DateOnly.FromDateTime(DateTime.UtcNow); // А если задали?
        Stocks = employeeRegistrationDTO.Stock;
        ForkliftControl = employeeRegistrationDTO.ForkliftControl;
        RolleyesControl = employeeRegistrationDTO.RolleyesControl;
        DateOfStartInTheCurrentPosition = (employeeRegistrationDTO.DateOfStartInTheCurrentPosition == null) ? DateOnly.FromDateTime(DateTime.UtcNow) : DateOnly.FromDateTime(DateTime.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentPosition.ToString()!)); // refactoring
        DateOfStartInTheCurrentStock = (employeeRegistrationDTO.DateOfStartInTheCurrentStock == null) ? null : DateOnly.FromDateTime(DateTime.Parse(employeeRegistrationDTO.DateOfStartInTheCurrentStock.ToString()!)); // А если не задано?
    }

    public Employee()
    {
    }
}
