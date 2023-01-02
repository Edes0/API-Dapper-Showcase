using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class ChangeNameOnCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Mimbox_Error_Log",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Event_Log",
                newName: "Created_At");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Mimbox_Error_Log",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Event_Log",
                newName: "Created");
        }
    }
}
