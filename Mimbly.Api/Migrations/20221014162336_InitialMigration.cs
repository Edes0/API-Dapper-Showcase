using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Parent_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mimbox_Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Region = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Postal_code = table.Column<string>(type: "Varchar(5)", nullable: false),
                    City = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Street_Address = table.Column<string>(type: "Nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mimbox_Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "Nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mimbox_Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "Nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company_Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Last_name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Phone_number = table.Column<string>(type: "Varchar(15)", nullable: false),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Contact_Company_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mimbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Water = table.Column<double>(type: "float", nullable: false),
                    Carbon = table.Column<double>(type: "float", nullable: false),
                    Plastic = table.Column<double>(type: "float", nullable: false),
                    Economy = table.Column<double>(type: "float", nullable: false),
                    Mimbox_Status_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mimbox_Model_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mimbox_Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Company_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mimbox_Company_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mimbox_Mimbox_Location_Mimbox_Location_Id",
                        column: x => x.Mimbox_Location_Id,
                        principalTable: "Mimbox_Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mimbox_Mimbox_Model_Mimbox_Model_Id",
                        column: x => x.Mimbox_Model_Id,
                        principalTable: "Mimbox_Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mimbox_Mimbox_Status_Mimbox_Status_Id",
                        column: x => x.Mimbox_Status_Id,
                        principalTable: "Mimbox_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mimbox_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Log = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "Date", nullable: false),
                    Mimbox_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mimbox_Log_Mimbox_Mimbox_Id",
                        column: x => x.Mimbox_Id,
                        principalTable: "Mimbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Contact_Company_Id",
                table: "Company_Contact",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Company_Id",
                table: "Mimbox",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Mimbox_Location_Id",
                table: "Mimbox",
                column: "Mimbox_Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Mimbox_Model_Id",
                table: "Mimbox",
                column: "Mimbox_Model_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Mimbox_Status_Id",
                table: "Mimbox",
                column: "Mimbox_Status_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_Log_Mimbox_Id",
                table: "Mimbox_Log",
                column: "Mimbox_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_Contact");

            migrationBuilder.DropTable(
                name: "Mimbox_Log");

            migrationBuilder.DropTable(
                name: "Mimbox");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Mimbox_Location");

            migrationBuilder.DropTable(
                name: "Mimbox_Model");

            migrationBuilder.DropTable(
                name: "Mimbox_Status");
        }
    }
}
