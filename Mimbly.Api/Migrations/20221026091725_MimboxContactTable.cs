using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class MimboxContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Company_Contact",
                type: "Nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Mimbox_Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    First_name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Last_name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Phone_number = table.Column<string>(type: "Varchar(15)", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mimbox_Contact_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Contact_Mimbox_Id",
                table: "Mimbox_Contact",
                column: "Mimbox_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mimbox_Contact");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Company_Contact");
        }
    }
}
