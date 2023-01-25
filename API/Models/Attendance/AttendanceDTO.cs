public class AttendanceDTO
{
    public AttendaceEmployee Employee { get; set; } = null!;

    public List<AttendanceShiftInfoDTO> Shifts { get; set; } = null!;
}