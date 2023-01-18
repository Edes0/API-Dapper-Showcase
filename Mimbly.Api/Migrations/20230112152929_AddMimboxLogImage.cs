using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class AddMimboxLogImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Log",
                table: "Mimbox_Log",
                type: "Nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Mimbox_Log_Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Mimbox_Log_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Log_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mimbox_Log_Image_Mimbox_Log_Mimbox_Log_Id",
                        column: x => x.Mimbox_Log_Id,
                        principalTable: "Mimbox_Log",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Log_Image_Mimbox_Log_Id",
                table: "Mimbox_Log_Image",
                column: "Mimbox_Log_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mimbox_Log_Image");

            migrationBuilder.AlterColumn<string>(
                name: "Log",
                table: "Mimbox_Log",
                type: "Nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Nvarchar(max)",
                oldNullable: true);
        }
    }
}
