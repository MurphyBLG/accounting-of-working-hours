public class ShiftCloseDTO
{
    public Guid ShiftId { get; set; }

    public Dictionary<string, int> WorkedHours { get; set; } = new();
}