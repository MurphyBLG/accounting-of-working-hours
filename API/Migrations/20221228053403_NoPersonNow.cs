using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NoPersonNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "employee_person_id_fkey",
                table: "employee");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropIndex(
                name: "IX_employee_person_id",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "employee");

            migrationBuilder.AddColumn<DateOnly>(
                name: "birthday",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "date_of_termination",
                table: "employee",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "passport_issue_date",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "passport_issuer",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passport_number",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "start_of_luch_seniority",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "start_of_total_seniority",
                table: "employee",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "employee",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "employee_passport_number_key",
                table: "employee",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employee_password_key",
                table: "employee",
                column: "password",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "employee_passport_number_key",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "employee_password_key",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "birthday",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "date_of_termination",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "name",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "passport_issue_date",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "passport_issuer",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "passport_number",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "password",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "patronymic",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "start_of_luch_seniority",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "start_of_total_seniority",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "employee");

            migrationBuilder.AddColumn<Guid>(
                name: "person_id",
                table: "employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    personid = table.Column<Guid>(name: "person_id", type: "uuid", nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    dateoftermination = table.Column<DateOnly>(name: "date_of_termination", type: "date", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    passportissuedate = table.Column<DateOnly>(name: "passport_issue_date", type: "date", nullable: false),
                    passportissuer = table.Column<string>(name: "passport_issuer", type: "character varying(255)", maxLength: 255, nullable: true),
                    passportnumber = table.Column<string>(name: "passport_number", type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    startofluchseniority = table.Column<DateOnly>(name: "start_of_luch_seniority", type: "date", nullable: false),
                    startoftotalseniority = table.Column<DateOnly>(name: "start_of_total_seniority", type: "date", nullable: false),
                    surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("person_pkey", x => x.personid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_person_id",
                table: "employee",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_passport_number_key",
                table: "person",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "person_password_key",
                table: "person",
                column: "password",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "employee_person_id_fkey",
                table: "employee",
                column: "person_id",
                principalTable: "person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
