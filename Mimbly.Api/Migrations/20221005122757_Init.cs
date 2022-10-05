using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mimbly.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mimbox",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    first_name = table.Column<string>(type: "Char(108)", nullable: true),
                    last_name = table.Column<string>(type: "Char(108)", nullable: true),
                    age = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mimbox", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    email = table.Column<string>(type: "CHAR(128)", nullable: false),
                    password = table.Column<string>(type: "CHAR(255)", nullable: false),
                    first_name = table.Column<string>(type: "CHAR(128)", nullable: true),
                    last_name = table.Column<string>(type: "CHAR(128)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    refresh_token = table.Column<string>(type: "Char(255)", nullable: false),
                    token_set_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Mimbox",
                columns: new[] { "id", "age", "first_name", "last_name" },
                values: new object[] { "496e6f57-3059-418c-9470-ce832269d656", (byte)31, "Daniel", "Persson" });

            migrationBuilder.InsertData(
                table: "Mimbox",
                columns: new[] { "id", "age", "first_name", "last_name" },
                values: new object[] { "61854b29-390e-42fe-b418-452db1603801", (byte)33, "Rundberg", "Rundbergsson" });

            migrationBuilder.CreateIndex(
                name: "IX_Mimbox_age",
                table: "Mimbox",
                column: "age");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_refresh_token_user_id",
                table: "refresh_token",
                columns: new[] { "refresh_token", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_user_id",
                table: "refresh_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mimbox");

            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
