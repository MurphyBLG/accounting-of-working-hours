using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ShiftHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "shift_info_shift_id_fkey",
                table: "shift_infos");

            migrationBuilder.RenameColumn(
                name: "shift_id",
                table: "shift_infos",
                newName: "shift_history_id");

            migrationBuilder.RenameIndex(
                name: "IX_shift_infos_shift_id",
                table: "shift_infos",
                newName: "IX_shift_infos_shift_history_id");

            migrationBuilder.CreateTable(
                name: "shift_history",
                columns: table => new
                {
                    ShiftHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    shiftid = table.Column<Guid>(name: "shift_id", type: "uuid", nullable: false),
                    stockid = table.Column<int>(name: "stock_id", type: "integer", nullable: false),
                    employeewhopostedtheshiftid = table.Column<Guid>(name: "employee_who_posted_the_shift_id", type: "uuid", nullable: false),
                    dayornight = table.Column<string>(name: "day_or_night", type: "character varying(255)", maxLength: 255, nullable: false),
                    openingdateandtime = table.Column<DateTime>(name: "opening_date_and_time", type: "timestamp with time zone", nullable: true),
                    employees = table.Column<string>(type: "jsonb", nullable: false),
                    closingdateandtime = table.Column<DateTime>(name: "closing_date_and_time", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shift_history_id", x => x.ShiftHistoryId);
                    table.ForeignKey(
                        name: "FK_shift_history_employee_employee_who_posted_the_shift_id",
                        column: x => x.employeewhopostedtheshiftid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shift_history_stock_stock_id",
                        column: x => x.stockid,
                        principalTable: "stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shift_history_employee_who_posted_the_shift_id",
                table: "shift_history",
                column: "employee_who_posted_the_shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_history_stock_id",
                table: "shift_history",
                column: "stock_id");

            migrationBuilder.AddForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos",
                column: "shift_history_id",
                principalTable: "shift_history",
                principalColumn: "ShiftHistoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos");

            migrationBuilder.DropTable(
                name: "shift_history");

            migrationBuilder.RenameColumn(
                name: "shift_history_id",
                table: "shift_infos",
                newName: "shift_id");

            migrationBuilder.RenameIndex(
                name: "IX_shift_infos_shift_history_id",
                table: "shift_infos",
                newName: "IX_shift_infos_shift_id");

            migrationBuilder.AddForeignKey(
                name: "shift_info_shift_id_fkey",
                table: "shift_infos",
                column: "shift_id",
                principalTable: "shifts",
                principalColumn: "shift_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
