using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddStatsUpdatedAtNULLABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Mimbox_Log",
                newName: "Created_At");

            migrationBuilder.AddColumn<DateTime>(
                name: "Stats_Updated_At",
                table: "Mimbox",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stats_Updated_At",
                table: "Mimbox");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Mimbox_Log",
                newName: "Created");
        }
    }
}
