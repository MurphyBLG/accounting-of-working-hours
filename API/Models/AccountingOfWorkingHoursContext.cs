using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class AccountingOfWorkingHoursContext : DbContext
{
    public AccountingOfWorkingHoursContext()
    {
    }

    public AccountingOfWorkingHoursContext(DbContextOptions<AccountingOfWorkingHoursContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;

    public virtual DbSet<EmployeeHistory> EmployeeHistories { get; set; } = null!;

    public virtual DbSet<Position> Positions { get; set; } = null!;

    public virtual DbSet<StocksHistory> StocksHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.DateOfTermination).HasColumnName("date_of_termination");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .HasColumnName("surname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .HasColumnName("patronymic");
            entity.Property(e => e.PassportIssueDate).HasColumnName("passport_issue_date");
            entity.Property(e => e.PassportIssuer)
                .HasMaxLength(255)
                .HasColumnName("passport_issuer");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(255)
                .HasColumnName("passport_number");

            entity.HasIndex(e => e.PassportNumber, "employee_passport_number_key").IsUnique();

            entity.Property(e => e.StartOfLuchSeniority).HasColumnName("start_of_luch_seniority");
            entity.Property(e => e.StartOfTotalSeniority).HasColumnName("start_of_total_seniority");
            
            entity.Property(e => e.DateOfStartInTheCurrentPosition).HasColumnName("date_of_start_in_the_current_position");
            entity.Property(e => e.DateOfStartInTheCurrentStock).HasColumnName("date_of_start_in_the_current_stock");
            entity.Property(e => e.ForkliftControl).HasColumnName("forklift_control");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.PercentageOfSalaryInAdvance).HasColumnName("percentage_of_salary_in_advance");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.QuarterlyBonus)
                .HasPrecision(10, 2)
                .HasColumnName("quarterly_bonus");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
            entity.Property(e => e.RolleyesControl).HasColumnName("rolleyes_control");
            entity.Property(e => e.Stock)
                .HasMaxLength(255)
                .HasColumnName("stock");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employee_position_id_fkey");
        });

        modelBuilder.Entity<EmployeeHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeHistoryId).HasName("employee_history_pkey");

            entity.ToTable("employee_history");

            entity.Property(e => e.EmployeeHistoryId).HasColumnName("employee_history_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDateOfWorkInCurrentPosition).HasColumnName("end_date_of_work_in_current_position");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.StartDateOfWorkInCurrentPosition).HasColumnName("start_date_of_work_in_current_position");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employee_history_employee_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.EmployeeHistories)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employee_history_position_id_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("position_pkey");

            entity.ToTable("position");

            entity.HasIndex(e => e.Name, "position_name_key").IsUnique();

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.InterfaceAccesses)
                .HasColumnType("jsonb")
                .HasColumnName("interface_accesses");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.QuarterlyBonus)
                .HasPrecision(10, 2)
                .HasColumnName("quarterly_bonus");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
        });

        modelBuilder.Entity<StocksHistory>(entity =>
        {
            entity.HasKey(e => e.StockHistoryId).HasName("stocks_history_pkey");

            entity.ToTable("stocks_history");

            entity.Property(e => e.StockHistoryId).HasColumnName("stock_history_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDateOfWorkInTheStock).HasColumnName("end_date_of_work_in_the_stock");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.StartDateOfWorkInTheStock).HasColumnName("start_date_of_work_in_the_stock");
            entity.Property(e => e.Stock)
                .HasMaxLength(255)
                .HasColumnName("stock");

            entity.HasOne(d => d.Employee).WithMany(p => p.StocksHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stocks_history_employee_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.StocksHistories)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stocks_history_position_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
