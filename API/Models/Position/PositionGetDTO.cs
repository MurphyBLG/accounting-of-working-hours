using API.Models;

public class PositionGetDTO
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public string InterfaceAccesses { get; set; } = null!;

    public PositionGetDTO()
    {

    }

    public PositionGetDTO(Employee currentEmployee)
    {
        PositionId = currentEmployee.PositionId;
        Name = currentEmployee.Position!.Name;
        Salary = currentEmployee.Position!.Salary;
        QuarterlyBonus = currentEmployee.Position!.QuarterlyBonus;
        InterfaceAccesses = currentEmployee.Position!.InterfaceAccesses;
    }

    public PositionGetDTO(Position position)
    {
        PositionId = position.PositionId;
        Name = position.Name;
        Salary = position.Salary;
        QuarterlyBonus = position.QuarterlyBonus;
        InterfaceAccesses = position.InterfaceAccesses;
    }
}