public class ShiftUpdateDTO
{
    public List<string> Employees { get; set; } = new();

    public string DayOrNight { get; set; } = null!;
}