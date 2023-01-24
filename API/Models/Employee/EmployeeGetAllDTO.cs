public class EmployeeGetAllDTO
{
    public Guid EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public List<StockDTO>? Stocks { get; set; }

    public string? Link { get; set; }
}