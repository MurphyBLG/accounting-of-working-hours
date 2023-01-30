using System.Text.Json;
using Newtonsoft.Json;

namespace API.Models;

public partial class Position
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public string InterfaceAccesses { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public Position()
    {

    }

    public Position(PositionPostDTO positionPostDTO)
    {
        PositionId = Guid.NewGuid();
        Name = positionPostDTO.Name;
        Salary = positionPostDTO.Salary;
        QuarterlyBonus = positionPostDTO.QuarterlyBonus;
        InterfaceAccesses = System.Text.Json.JsonSerializer.Serialize(positionPostDTO.InterfaceAccesses);
    }
}
