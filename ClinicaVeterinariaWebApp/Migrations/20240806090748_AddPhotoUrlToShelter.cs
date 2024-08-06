using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaVeterinariaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrlToShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Shelters",
                newName: "PhotoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Shelters",
                newName: "PhotoPath");
        }
    }
}
