using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaVeterinariaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class updateProductEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drawers_DrawerId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "DrawerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drawers_DrawerId",
                table: "Products",
                column: "DrawerId",
                principalTable: "Drawers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drawers_DrawerId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "DrawerId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drawers_DrawerId",
                table: "Products",
                column: "DrawerId",
                principalTable: "Drawers",
                principalColumn: "Id");
        }
    }
}
