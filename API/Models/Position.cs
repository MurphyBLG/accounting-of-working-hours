﻿namespace API.Models;

public partial class Position
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public string InterfaceAccesses { get; set; } = null!;

    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<StocksHistory> StocksHistories { get; } = new List<StocksHistory>();
}
