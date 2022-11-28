using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddEntitiesForFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street_Address",
                table: "Mimbox_Location",
                newName: "Street_address");

            migrationBuilder.RenameColumn(
                name: "Water",
                table: "Mimbox",
                newName: "Water_Saved");

            migrationBuilder.RenameColumn(
                name: "Plastic",
                table: "Mimbox",
                newName: "Plastic_Saved");

            migrationBuilder.RenameColumn(
                name: "Economy",
                table: "Mimbox",
                newName: "Economy_Saved");

            migrationBuilder.RenameColumn(
                name: "Co2",
                table: "Mimbox",
                newName: "Co2_Saved");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Mimbox_Log",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.CreateTable(
                name: "Error_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discarded = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Error_Log_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Log_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wash_Stats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Water_saved = table.Column<double>(type: "float", nullable: false),
                    Co2_saved = table.Column<double>(type: "float", nullable: false),
                    Plastic_saved = table.Column<double>(type: "float", nullable: false),
                    Economy_saved = table.Column<double>(type: "float", nullable: false),
                    Started_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ended_At = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Washing_Machine_Id = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wash_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wash_Stats_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Error_Log_Mimbox_Id",
                table: "Error_Log",
                column: "Mimbox_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Log_Mimbox_Id",
                table: "Event_Log",
                column: "Mimbox_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wash_Stats_Mimbox_Id",
                table: "Wash_Stats",
                column: "Mimbox_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Error_Log");

            migrationBuilder.DropTable(
                name: "Event_Log");

            migrationBuilder.DropTable(
                name: "Wash_Stats");

            migrationBuilder.RenameColumn(
                name: "Street_address",
                table: "Mimbox_Location",
                newName: "Street_Address");

            migrationBuilder.RenameColumn(
                name: "Water_Saved",
                table: "Mimbox",
                newName: "Water");

            migrationBuilder.RenameColumn(
                name: "Plastic_Saved",
                table: "Mimbox",
                newName: "Plastic");

            migrationBuilder.RenameColumn(
                name: "Economy_Saved",
                table: "Mimbox",
                newName: "Economy");

            migrationBuilder.RenameColumn(
                name: "Co2_Saved",
                table: "Mimbox",
                newName: "Co2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Mimbox_Log",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
