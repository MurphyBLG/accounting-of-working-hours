using API.Models;
using Newtonsoft.Json;

public class PositionGetDTO
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public InterfaceAccesses InterfaceAccesses { get; set; } = null!;

    public PositionGetDTO()
    {

    }

    public PositionGetDTO(Employee currentEmployee)
    {
        PositionId = currentEmployee.PositionId;
        Name = currentEmployee.Position!.Name;
        Salary = currentEmployee.Position!.Salary;
        QuarterlyBonus = currentEmployee.Position!.QuarterlyBonus;
        InterfaceAccesses = JsonConvert.DeserializeObject<InterfaceAccesses>(currentEmployee.Position!.InterfaceAccesses)!;
    }

    public PositionGetDTO(Position position)
    {
        PositionId = position.PositionId;
        Name = position.Name;
        Salary = position.Salary;
        QuarterlyBonus = position.QuarterlyBonus;
        InterfaceAccesses = JsonConvert.DeserializeObject<InterfaceAccesses>(position.InterfaceAccesses)!;
    }
}