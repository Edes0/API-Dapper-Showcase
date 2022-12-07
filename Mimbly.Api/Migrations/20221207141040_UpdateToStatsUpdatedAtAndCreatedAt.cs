using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class UpdateToStatsUpdatedAtAndCreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Mimbox");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Mimbox_Log",
                newName: "Created_At");

            migrationBuilder.AddColumn<DateTime>(
                name: "Stats_Updated_At",
                table: "Mimbox",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Mimbox",
                type: "datetime",
                nullable: true);
        }
    }
}
