using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ShiftInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shift_infos",
                columns: table => new
                {
                    shiftinfoid = table.Column<Guid>(name: "shift_info_id", type: "uuid", nullable: false),
                    shiftid = table.Column<Guid>(name: "shift_id", type: "uuid", nullable: false),
                    employeeid = table.Column<Guid>(name: "employee_id", type: "uuid", nullable: false),
                    dateandtimeofarrival = table.Column<DateTime>(name: "date_and_time_of_arrival", type: "timestamp with time zone", nullable: false),
                    numberofhoursworked = table.Column<int>(name: "number_of_hours_worked", type: "integer", nullable: false),
                    penalty = table.Column<decimal>(type: "numeric", nullable: true),
                    penaltycomment = table.Column<string>(name: "penalty_comment", type: "character varying(255)", maxLength: 255, nullable: true),
                    send = table.Column<decimal>(type: "numeric", nullable: true),
                    sendcomment = table.Column<string>(name: "send_comment", type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_infos", x => x.shiftinfoid);
                    table.ForeignKey(
                        name: "shift_info_employee_id_fkey",
                        column: x => x.employeeid,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "shift_info_shift_id_fkey",
                        column: x => x.shiftid,
                        principalTable: "shifts",
                        principalColumn: "shift_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_date_and_time_of_arrival_employee_id",
                table: "shift_infos",
                columns: new[] { "date_and_time_of_arrival", "employee_id" });

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_employee_id",
                table: "shift_infos",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_shift_id",
                table: "shift_infos",
                column: "shift_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shift_infos");
        }
    }
}
