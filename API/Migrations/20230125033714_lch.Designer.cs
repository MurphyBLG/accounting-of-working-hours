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
    [Migration("20230125033714_lch")]
    partial class lch
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

                    b.Property<string>("Stocks")
                        .HasColumnType("jsonb")
                        .HasColumnName("stocks");

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

                    b.Property<string>("Stocks")
                        .HasColumnType("jsonb")
                        .HasColumnName("stocks");

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

            modelBuilder.Entity("Mark", b =>
                {
                    b.Property<Guid>("MarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("MarkDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("mark_date");

                    b.Property<int>("StockId")
                        .HasColumnType("integer")
                        .HasColumnName("stock_id");

                    b.HasKey("MarkId")
                        .HasName("mark_id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StockId");

                    b.ToTable("marks", (string)null);
                });

            modelBuilder.Entity("Shift", b =>
                {
                    b.Property<Guid>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("shift_id");

                    b.Property<DateTime?>("ClosingDateAndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closing_date_and_time");

                    b.Property<string>("DayOrNight")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("day_or_night");

                    b.Property<Guid>("EmployeeWhoPostedTheShiftId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_who_posted_the_shift_id");

                    b.Property<string>("Employees")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("employees");

                    b.Property<DateTime?>("OpeningDateAndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("opening_date_and_time");

                    b.Property<int>("StockId")
                        .HasColumnType("integer")
                        .HasColumnName("stock_id");

                    b.HasKey("ShiftId")
                        .HasName("shift_id");

                    b.HasIndex("EmployeeWhoPostedTheShiftId");

                    b.HasIndex("StockId");

                    b.ToTable("shifts", (string)null);
                });

            modelBuilder.Entity("ShiftHistory", b =>
                {
                    b.Property<Guid>("ShiftHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ClosingDateAndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closing_date_and_time");

                    b.Property<string>("DayOrNight")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("day_or_night");

                    b.Property<Guid>("EmployeeWhoPostedTheShiftId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_who_posted_the_shift_id");

                    b.Property<string>("Employees")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("employees");

                    b.Property<DateTime?>("OpeningDateAndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("opening_date_and_time");

                    b.Property<int>("StockId")
                        .HasColumnType("integer")
                        .HasColumnName("stock_id");

                    b.HasKey("ShiftHistoryId")
                        .HasName("shift_history_id");

                    b.HasIndex("EmployeeWhoPostedTheShiftId");

                    b.HasIndex("StockId");

                    b.ToTable("shift_history", (string)null);
                });

            modelBuilder.Entity("ShiftInfo", b =>
                {
                    b.Property<Guid>("ShiftInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("shift_info_id");

                    b.Property<DateTime>("DateAndTimeOfArrival")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_and_time_of_arrival");

                    b.Property<string>("DayOrNight")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("day_or_night");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("employee_id");

                    b.Property<int>("NumberOfHoursWorked")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_hours_worked");

                    b.Property<decimal?>("Penalty")
                        .HasColumnType("numeric")
                        .HasColumnName("penalty");

                    b.Property<string>("PenaltyComment")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("penalty_comment");

                    b.Property<decimal?>("Send")
                        .HasColumnType("numeric")
                        .HasColumnName("send");

                    b.Property<string>("SendComment")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("send_comment");

                    b.Property<Guid?>("ShiftHistoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("shift_history_id");

                    b.HasKey("ShiftInfoId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ShiftHistoryId");

                    b.HasIndex("DateAndTimeOfArrival", "EmployeeId");

                    b.ToTable("shift_infos", (string)null);
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StockId"));

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("links");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("StockId")
                        .HasName("stock_id");

                    b.ToTable("stock", (string)null);
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

            modelBuilder.Entity("Mark", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("Marks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock", "Stock")
                        .WithMany("Marks")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Shift", b =>
                {
                    b.HasOne("API.Models.Employee", "EmployeeWhoPostedTheShift")
                        .WithMany("Shifts")
                        .HasForeignKey("EmployeeWhoPostedTheShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("shift_employee_who_posted_the_shift_id_fkey");

                    b.HasOne("Stock", "Stock")
                        .WithMany("Shifts")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("shift_stock_id_fkey");

                    b.Navigation("EmployeeWhoPostedTheShift");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("ShiftHistory", b =>
                {
                    b.HasOne("API.Models.Employee", "EmployeeWhoPostedTheShift")
                        .WithMany("ShiftHistories")
                        .HasForeignKey("EmployeeWhoPostedTheShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock", "Stock")
                        .WithMany("ShiftHistories")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeWhoPostedTheShift");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("ShiftInfo", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("ShiftInfos")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("shift_info_employee_id_fkey");

                    b.HasOne("ShiftHistory", "ShiftHistory")
                        .WithMany("ShiftInfos")
                        .HasForeignKey("ShiftHistoryId")
                        .HasConstraintName("shift_info_shift_history_id_fkey");

                    b.Navigation("Employee");

                    b.Navigation("ShiftHistory");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("Marks");

                    b.Navigation("ShiftHistories");

                    b.Navigation("ShiftInfos");

                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("API.Models.Position", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ShiftHistory", b =>
                {
                    b.Navigation("ShiftInfos");
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.Navigation("Marks");

                    b.Navigation("ShiftHistories");

                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
