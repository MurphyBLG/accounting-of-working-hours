using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Shifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    shiftid = table.Column<Guid>(name: "shift_id", type: "uuid", nullable: false),
                    stockid = table.Column<int>(name: "stock_id", type: "integer", nullable: false),
                    employeewhopostedtheshiftid = table.Column<Guid>(name: "employee_who_posted_the_shift_id", type: "uuid", nullable: false),
                    dayornight = table.Column<string>(name: "day_or_night", type: "character varying(255)", maxLength: 255, nullable: false),
                    openingdateandtime = table.Column<DateTime>(name: "opening_date_and_time", type: "timestamp with time zone", nullable: false),
                    employees = table.Column<string>(type: "jsonb", nullable: false),
                    closingdateandtime = table.Column<DateTime>(name: "closing_date_and_time", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shift_id", x => x.shiftid);
                    table.ForeignKey(
                        name: "shift_employee_who_posted_the_shift_id_fkey",
                        column: x => x.employeewhopostedtheshiftid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "shift_stock_id_fkey",
                        column: x => x.stockid,
                        principalTable: "stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shifts_employee_who_posted_the_shift_id",
                table: "shifts",
                column: "employee_who_posted_the_shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shifts_stock_id",
                table: "shifts",
                column: "stock_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shifts");
        }
    }
}
