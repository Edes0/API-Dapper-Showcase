using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddTotalWashesAndTotalTapNULLABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Mimbox",
                type: "Nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Nvarchar(50)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Mimbox",
                type: "Nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Nvarchar(50)");
        }
    }
}
