public class ShiftOpenDTO
{
    public int StockId { get; set; }

    public List<string> Employees { get; set; } = new();

    public string DayOrNight { get; set; } = null!;
}