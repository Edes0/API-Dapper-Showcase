using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddErrorLog_EventLog_MimboxEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Water_To_Mimbox_Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Started_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ended_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water_To_Mimbox_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Water_To_Mimbox_Event_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Water_To_Washing_Machine_Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    water_saved = table.Column<double>(type: "float", nullable: false),
                    co2_saved = table.Column<double>(type: "float", nullable: false),
                    plastic_saved = table.Column<double>(type: "float", nullable: false),
                    Economy_saved = table.Column<double>(type: "float", nullable: false),
                    Started_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ended_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Washing_Machine_Id = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water_To_Washing_Machine_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Water_To_Washing_Machine_Event_Mimbox_Mimbox_Id",
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
                name: "IX_Water_To_Mimbox_Event_Mimbox_Id",
                table: "Water_To_Mimbox_Event",
                column: "Mimbox_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Water_To_Washing_Machine_Event_Mimbox_Id",
                table: "Water_To_Washing_Machine_Event",
                column: "Mimbox_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Error_Log");

            migrationBuilder.DropTable(
                name: "Event_Log");

            migrationBuilder.DropTable(
                name: "Water_To_Mimbox_Event");

            migrationBuilder.DropTable(
                name: "Water_To_Washing_Machine_Event");

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
