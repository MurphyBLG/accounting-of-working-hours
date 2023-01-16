﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AccountingOfWorkingHoursContext))]
    partial class AccountingOfWorkingHoursContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<DateOnly?>("DateOfStartInTheCurrentLink")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateOfStartInTheCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("date_of_start_in_the_current_position");

                    b.Property<DateOnly?>("DateOfStartInTheCurrentStock")
                        .HasColumnType("date")
                        .HasColumnName("date_of_start_in_the_current_stock");

                    b.Property<DateOnly?>("DateOfTermination")
                        .HasColumnType("date")
                        .HasColumnName("date_of_termination");

                    b.Property<bool>("ForkliftControl")
                        .HasColumnType("boolean")
                        .HasColumnName("forklift_control");

                    b.Property<string>("Link")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("link");

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

                    b.Property<int>("Password")
                        .HasColumnType("integer");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("patronymic");

                    b.Property<int>("PercentageOfSalaryInAdvance")
                        .HasColumnType("integer")
                        .HasColumnName("percentage_of_salary_in_advance");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<decimal>("QuarterlyBonus")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("quarterly_bonus");

                    b.Property<bool>("RolleyesControl")
                        .HasColumnType("boolean")
                        .HasColumnName("rolleyes_control");

                    b.Property<decimal>("Salary")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("salary");

                    b.Property<DateOnly>("StartOfLuchSeniority")
                        .HasColumnType("date")
                        .HasColumnName("start_of_luch_seniority");

                    b.Property<DateOnly>("StartOfTotalSeniority")
                        .HasColumnType("date")
                        .HasColumnName("start_of_total_seniority");

                    b.Property<string>("Stock")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stock");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("surname");

                    b.HasKey("EmployeeId")
                        .HasName("employee_pkey");

                    b.HasIndex("PositionId");

                    b.HasIndex(new[] { "PassportNumber" }, "employee_passport_number_key")
                        .IsUnique();

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("API.Models.EmployeeHistory", b =>
                {
                    b.Property<Guid?>("EmployeeHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("employee_history_id");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly?>("DateOfTermination")
                        .HasColumnType("date");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateOnly?>("EndDateOfWorkInCurrentLink")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("EndDateOfWorkInCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("end_date_of_work_in_current_position");

                    b.Property<DateOnly?>("EndDateOfWorkInCurrentStock")
                        .HasColumnType("date")
                        .HasColumnName("end_date_of_work_in_stock");

                    b.Property<bool>("ForkliftControl")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PassportIssueDate")
                        .HasColumnType("date");

                    b.Property<string>("PassportIssuer")
                        .HasColumnType("text");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PercentageOfSalaryInAdvance")
                        .HasColumnType("integer");

                    b.Property<Guid?>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<decimal>("QuarterlyBonus")
                        .HasColumnType("numeric");

                    b.Property<bool>("RolleyesControl")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.Property<DateOnly?>("StartDateOfWorkInCurrentLink")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("StartDateOfWorkInCurrentPosition")
                        .HasColumnType("date")
                        .HasColumnName("start_date_of_work_in_current_position");

                    b.Property<DateOnly?>("StartDateOfWorkIncurrentStock")
                        .HasColumnType("date")
                        .HasColumnName("start_date_of_work_in_stock");

                    b.Property<DateOnly>("StartOfLuchSeniority")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartOfTotalSeniority")
                        .HasColumnType("date");

                    b.Property<string>("Stock")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stock");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EmployeeHistoryId")
                        .HasName("employee_history_pkey");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("employee_history", (string)null);
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

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("employee_position_id_fkey");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("API.Models.EmployeeHistory", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("employee_history_employee_id_fkey");

                    b.HasOne("API.Models.Position", "Position")
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("employee_history_position_id_fkey");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("EmployeeHistories");
                });

            modelBuilder.Entity("API.Models.Position", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
