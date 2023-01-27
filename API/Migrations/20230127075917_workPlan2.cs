using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class workPlan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number_of_hors_per_night_shift",
                table: "work_plans",
                newName: "number_of_hours_per_night_shift");

            migrationBuilder.RenameColumn(
                name: "number_of_hors_per_day_shift",
                table: "work_plans",
                newName: "number_of_hours_per_day_shift");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number_of_hours_per_night_shift",
                table: "work_plans",
                newName: "number_of_hors_per_night_shift");

            migrationBuilder.RenameColumn(
                name: "number_of_hours_per_day_shift",
                table: "work_plans",
                newName: "number_of_hors_per_day_shift");
        }
    }
}
