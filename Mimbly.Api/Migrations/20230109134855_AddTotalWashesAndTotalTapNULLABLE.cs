using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddTotalWashesAndTotalTapNULLABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total_Tap",
                table: "Mimbox",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total_Washes",
                table: "Mimbox",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Tap",
                table: "Mimbox");

            migrationBuilder.DropColumn(
                name: "Total_Washes",
                table: "Mimbox");
        }
    }
}
