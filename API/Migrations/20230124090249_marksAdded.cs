using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class marksAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    MarkId = table.Column<Guid>(type: "uuid", nullable: false),
                    stockid = table.Column<int>(name: "stock_id", type: "integer", nullable: false),
                    employeeid = table.Column<Guid>(name: "employee_id", type: "uuid", nullable: false),
                    markdate = table.Column<DateTime>(name: "mark_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("mark_id", x => x.MarkId);
                    table.ForeignKey(
                        name: "FK_marks_employee_employee_id",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_marks_stock_stock_id",
                        column: x => x.stockid,
                        principalTable: "stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_marks_employee_id",
                table: "marks",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_marks_stock_id",
                table: "marks",
                column: "stock_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "marks");
        }
    }
}
