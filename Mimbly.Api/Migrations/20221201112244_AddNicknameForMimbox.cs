using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddNicknameForMimbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Error_Log");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Mimbox",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mimbox_Error_Log",
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
                    table.PrimaryKey("PK_Mimbox_Error_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mimbox_Error_Log_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Error_Log_Mimbox_Id",
                table: "Mimbox_Error_Log",
                column: "Mimbox_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mimbox_Error_Log");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Mimbox");

            migrationBuilder.CreateTable(
                name: "Error_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Discarded = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(50)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Error_Log_Mimbox_Id",
                table: "Error_Log",
                column: "Mimbox_Id");
        }
    }
}
