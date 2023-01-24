public class ShiftGetCurrentDTO
{
    public Guid ShiftId { get; set; }

    public string DayOrNight { get; set; } = null!;

    public List<ShiftEmployeeInfoDTO> Employees { get; set; } = new ();
}