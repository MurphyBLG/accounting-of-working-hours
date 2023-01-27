using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class workPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "work_plans",
                columns: table => new
                {
                    WorkPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    employeeid = table.Column<Guid>(name: "employee_id", type: "uuid", nullable: false),
                    numberofdayshifts = table.Column<int>(name: "number_of_day_shifts", type: "integer", nullable: false),
                    numberofhorsperdayshift = table.Column<int>(name: "number_of_hors_per_day_shift", type: "integer", nullable: false),
                    numberofnightshifts = table.Column<int>(name: "number_of_night_shifts", type: "integer", nullable: false),
                    numberofhorspernightshift = table.Column<int>(name: "number_of_hors_per_night_shift", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_plans", x => x.WorkPlanId);
                    table.ForeignKey(
                        name: "FK_work_plans_employee_employee_id",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_work_plans_employee_id",
                table: "work_plans",
                column: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "work_plans");
        }
    }
}
