public class PositionPostDTO
{
    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public InterfaceAccesses InterfaceAccesses { get; set; } = null!;
}