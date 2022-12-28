namespace API.Models;

public partial class StocksHistory
{
    public Guid? StockHistoryId { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? PositionId { get; set; }

    public string? Link { get; set; }

    public string Stock { get; set; } = null!;

    public DateOnly StartDateOfWorkInTheStock { get; set; }

    public DateOnly EndDateOfWorkInTheStock { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }
}
