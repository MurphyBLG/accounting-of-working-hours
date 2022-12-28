﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AccountingOfWorkingHoursContext))]
    [Migration("20221227111846_GuidsInsteadOfIDs1")]
    partial class GuidsInsteadOfIDs1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<Guid?>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly>("DateOfStartInTheCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("date_of_start_in_the_current_position");

                    b.Property<DateOnly>("DateOfStartInTheCurrentStock")
                        .HasColumnType("date")
                        .HasColumnName("date_of_start_in_the_current_stock");

                    b.Property<bool>("ForkliftControl")
                        .HasColumnType("boolean")
                        .HasColumnName("forklift_control");

                    b.Property<string>("Link")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("link");

                    b.Property<int>("PercentageOfSalaryInAdvance")
                        .HasColumnType("integer")
                        .HasColumnName("percentage_of_salary_in_advance");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<bool>("RolleyesControl")
                        .HasColumnType("boolean")
                        .HasColumnName("rolleyes_control");

                    b.Property<string>("Stock")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stock");

                    b.HasKey("EmployeeId")
                        .HasName("employee_pkey");

                    b.HasIndex("PersonId");

                    b.HasIndex("PositionId");

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("API.Models.EmployeeHistory", b =>
                {
                    b.Property<Guid?>("EmployeeHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("employee_history_id");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly>("EndDateOfWorkInCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("end_date_of_work_in_current_position");

                    b.Property<string>("Link")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("link");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<DateOnly>("StartDateOfWorkInCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("start_date_of_work_in_current_position");

                    b.HasKey("EmployeeHistoryId")
                        .HasName("employee_history_pkey");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("employee_history", (string)null);
                });

            modelBuilder.Entity("API.Models.Person", b =>
                {
                    b.Property<Guid?>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<DateOnly?>("DateOfTermination")
                        .HasColumnType("date")
                        .HasColumnName("date_of_termination");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<DateOnly>("PassportIssueDate")
                        .HasColumnType("date")
                        .HasColumnName("passport_issue_date");

                    b.Property<string>("PassportIssuer")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("passport_issuer");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("passport_number");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("patronymic");

                    b.Property<DateOnly>("StartOfLuchSeniority")
                        .HasColumnType("date")
                        .HasColumnName("start_of_luch_seniority");

                    b.Property<DateOnly>("StartOfTotalSeniority")
                        .HasColumnType("date")
                        .HasColumnName("start_of_total_seniority");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("surname");

                    b.HasKey("PersonId")
                        .HasName("person_pkey");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("API.Models.Position", b =>
                {
                    b.Property<Guid?>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<string>("InterfaceAccesses")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("interface_accesses");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<decimal>("QuarterlyBonus")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("quarterly_bonus");

                    b.Property<decimal>("Salary")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("salary");

                    b.HasKey("PositionId")
                        .HasName("position_pkey");

                    b.HasIndex(new[] { "Name" }, "position_name_key")
                        .IsUnique();

                    b.ToTable("position", (string)null);
                });

            modelBuilder.Entity("API.Models.StocksHistory", b =>
                {
                    b.Property<Guid?>("StockHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("stock_history_id");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly>("EndDateOfWorkInTheStock")
                        .HasColumnType("date")
                        .HasColumnName("end_date_of_work_in_the_stock");

                    b.Property<string>("Link")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("link");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<DateOnly>("StartDateOfWorkInTheStock")
                        .HasColumnType("date")
                        .HasColumnName("start_date_of_work_in_the_stock");

                    b.Property<string>("Stock")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stock");

                    b.HasKey("StockHistoryId")
                        .HasName("stocks_history_pkey");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("stocks_history", (string)null);
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Person", "Person")
                        .WithMany("Employees")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("employee_person_id_fkey");

                    b.HasOne("API.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("employee_position_id_fkey");

                    b.Navigation("Person");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("API.Models.EmployeeHistory", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("employee_history_employee_id_fkey");

                    b.HasOne("API.Models.Position", "Position")
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("employee_history_position_id_fkey");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("API.Models.StocksHistory", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("StocksHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("stocks_history_employee_id_fkey");

                    b.HasOne("API.Models.Position", "Position")
                        .WithMany("StocksHistories")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("stocks_history_position_id_fkey");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("StocksHistories");
                });

            modelBuilder.Entity("API.Models.Person", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("API.Models.Position", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("Employees");

                    b.Navigation("StocksHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
