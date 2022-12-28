using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class PasswordAndPassportNumberAreUniqueNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "person_passport_number_key",
                table: "person");

            migrationBuilder.DropIndex(
                name: "person_password_key",
                table: "person");
        }
    }
}
