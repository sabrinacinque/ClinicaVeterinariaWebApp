using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaVeterinariaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDischargeDateToShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DischargeDate",
                table: "Shelters",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DischargeDate",
                table: "Shelters");
        }
    }
}
