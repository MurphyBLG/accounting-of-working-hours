using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class workpls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    positionid = table.Column<Guid>(name: "position_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    salary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    quarterlybonus = table.Column<decimal>(name: "quarterly_bonus", type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    interfaceaccesses = table.Column<string>(name: "interface_accesses", type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("position_pkey", x => x.positionid);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employeeid = table.Column<int>(name: "employee_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    passportnumber = table.Column<string>(name: "passport_number", type: "character varying(255)", maxLength: 255, nullable: false),
                    passportissuer = table.Column<string>(name: "passport_issuer", type: "character varying(255)", maxLength: 255, nullable: true),
                    passportissuedate = table.Column<DateOnly>(name: "passport_issue_date", type: "date", nullable: false),
                    startoftotalseniority = table.Column<DateOnly>(name: "start_of_total_seniority", type: "date", nullable: false),
                    startofluchseniority = table.Column<DateOnly>(name: "start_of_luch_seniority", type: "date", nullable: false),
                    dateoftermination = table.Column<DateOnly>(name: "date_of_termination", type: "date", nullable: true),
                    positionid = table.Column<Guid>(name: "position_id", type: "uuid", nullable: true),
                    salary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    quarterlybonus = table.Column<decimal>(name: "quarterly_bonus", type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    percentageofsalaryinadvance = table.Column<int>(name: "percentage_of_salary_in_advance", type: "integer", nullable: false),
                    link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    stock = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    forkliftcontrol = table.Column<bool>(name: "forklift_control", type: "boolean", nullable: false),
                    rolleyescontrol = table.Column<bool>(name: "rolleyes_control", type: "boolean", nullable: false),
                    dateofstartinthecurrentposition = table.Column<DateOnly>(name: "date_of_start_in_the_current_position", type: "date", nullable: false),
                    dateofstartinthecurrentstock = table.Column<DateOnly>(name: "date_of_start_in_the_current_stock", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_pkey", x => x.employeeid);
                    table.ForeignKey(
                        name: "employee_position_id_fkey",
                        column: x => x.positionid,
                        principalTable: "position",
                        principalColumn: "position_id");
                });

            migrationBuilder.CreateTable(
                name: "employee_history",
                columns: table => new
                {
                    employeehistoryid = table.Column<Guid>(name: "employee_history_id", type: "uuid", nullable: false),
                    employeeid = table.Column<int>(name: "employee_id", type: "integer", nullable: false),
                    positionid = table.Column<Guid>(name: "position_id", type: "uuid", nullable: true),
                    link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    stock = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    startdateofworkincurrentposition = table.Column<DateOnly>(name: "start_date_of_work_in_current_position", type: "date", nullable: false),
                    enddateofworkincurrentposition = table.Column<DateOnly>(name: "end_date_of_work_in_current_position", type: "date", nullable: false),
                    startdateofworkinstock = table.Column<DateOnly>(name: "start_date_of_work_in_stock", type: "date", nullable: false),
                    enddateofworkinstock = table.Column<DateOnly>(name: "end_date_of_work_in_stock", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_history_pkey", x => x.employeehistoryid);
                    table.ForeignKey(
                        name: "employee_history_employee_id_fkey",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "employee_history_position_id_fkey",
                        column: x => x.positionid,
                        principalTable: "position",
                        principalColumn: "position_id");
                });

            migrationBuilder.CreateIndex(
                name: "employee_passport_number_key",
                table: "employee",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_position_id",
                table: "employee",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_history_employee_id",
                table: "employee_history",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_history_position_id",
                table: "employee_history",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "position_name_key",
                table: "position",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_history");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "position");
        }
    }
}
