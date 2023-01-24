using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class littleChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos");

            migrationBuilder.DropColumn(
                name: "shift_id",
                table: "shift_history");

            migrationBuilder.AlterColumn<Guid>(
                name: "shift_history_id",
                table: "shift_infos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos",
                column: "shift_history_id",
                principalTable: "shift_history",
                principalColumn: "ShiftHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos");

            migrationBuilder.AlterColumn<Guid>(
                name: "shift_history_id",
                table: "shift_infos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "shift_id",
                table: "shift_history",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "shift_info_shift_history_id_fkey",
                table: "shift_infos",
                column: "shift_history_id",
                principalTable: "shift_history",
                principalColumn: "ShiftHistoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
