using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class LargerUrlForMimboxLogImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Mimbox_Log_Image",
                type: "Nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Mimbox_Log_Image",
                type: "Nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(500)");
        }
    }
}
