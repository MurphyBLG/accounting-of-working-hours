using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NewEmpH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "start_date_of_work_in_stock",
                table: "employee_history",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "start_date_of_work_in_current_position",
                table: "employee_history",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date_of_work_in_stock",
                table: "employee_history",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date_of_work_in_current_position",
                table: "employee_history",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Birthday",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "employee_history",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfTermination",
                table: "employee_history",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDateOfWorkInCurrentLink",
                table: "employee_history",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForkliftControl",
                table: "employee_history",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "employee_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PassportIssueDate",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "PassportIssuer",
                table: "employee_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "employee_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "employee_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PercentageOfSalaryInAdvance",
                table: "employee_history",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "QuarterlyBonus",
                table: "employee_history",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "RolleyesControl",
                table: "employee_history",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "employee_history",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDateOfWorkInCurrentLink",
                table: "employee_history",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartOfLuchSeniority",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartOfTotalSeniority",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "employee_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_of_start_in_the_current_stock",
                table: "employee",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_of_start_in_the_current_position",
                table: "employee",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfStartInTheCurrentLink",
                table: "employee",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "DateOfTermination",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "EndDateOfWorkInCurrentLink",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "ForkliftControl",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "PassportIssueDate",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "PassportIssuer",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "PercentageOfSalaryInAdvance",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "QuarterlyBonus",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "RolleyesControl",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "StartDateOfWorkInCurrentLink",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "StartOfLuchSeniority",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "StartOfTotalSeniority",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "employee_history");

            migrationBuilder.DropColumn(
                name: "DateOfStartInTheCurrentLink",
                table: "employee");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "start_date_of_work_in_stock",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "start_date_of_work_in_current_position",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date_of_work_in_stock",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date_of_work_in_current_position",
                table: "employee_history",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_of_start_in_the_current_stock",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_of_start_in_the_current_position",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
