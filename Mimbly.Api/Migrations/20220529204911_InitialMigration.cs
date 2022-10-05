//#nullable disable

//namespace Mimbly.Api.Migrations;

//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//public partial class InitialMigration : Migration
//{
//    protected override void Up(MigrationBuilder migrationBuilder)
//    {
//        migrationBuilder.AlterDatabase()
//            .Annotation("MySql:CharSet", "utf8mb4");

//        migrationBuilder.CreateTable(
//            name: "Mimbly",
//            columns: table => new
//            {
//                id = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
//                first_name = table.Column<string>(type: "Char(108)", nullable: true)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                last_name = table.Column<string>(type: "Char(108)", nullable: true)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                age = table.Column<sbyte>(type: "TINYINT", nullable: false) 
//            },
//            constraints: table => table.PrimaryKey("PK_Mimbly", x => x.id))
//            .Annotation("MySql:CharSet", "utf8mb4");

//        migrationBuilder.CreateTable(
//            name: "user",
//            columns: table => new
//            {
//                id = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
//                email = table.Column<string>(type: "CHAR(128)", nullable: false)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                password = table.Column<string>(type: "CHAR(255)", nullable: false)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                first_name = table.Column<string>(type: "CHAR(128)", nullable: true)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                last_name = table.Column<string>(type: "CHAR(128)", nullable: true)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
//                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
//                updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
//                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
//            },
//            constraints: table => table.PrimaryKey("PK_user", x => x.id))
//            .Annotation("MySql:CharSet", "utf8mb4");

//        migrationBuilder.CreateTable(
//            name: "refresh_token",
//            columns: table => new
//            {
//                id = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
//                user_id = table.Column<Guid>(type: "Char(36)", nullable: false, collation: "ascii_general_ci"),
//                refresh_token = table.Column<string>(type: "Char(255)", nullable: false)
//                    .Annotation("MySql:CharSet", "utf8mb4"),
//                token_set_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
//                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
//            },
//            constraints: table =>
//            {
//                table.PrimaryKey("PK_refresh_token", x => x.id);
//                table.ForeignKey(
//                    name: "FK_refresh_token_user_user_id",
//                    column: x => x.user_id,
//                    principalTable: "user",
//                    principalColumn: "id",
//                    onDelete: ReferentialAction.Cascade);
//            })
//            .Annotation("MySql:CharSet", "utf8mb4");

//        migrationBuilder.InsertData(
//            table: "Mimbly",
//            columns: new[] { "id", "age", "first_name", "last_name" },
//            values: new object[] { new Guid("70a8cb1e-9fca-42d7-8310-b78188655509"), (sbyte)31, "Daniel", "Persson" });

//        migrationBuilder.InsertData(
//            table: "Mimbly",
//            columns: new[] { "id", "age", "first_name", "last_name" },
//            values: new object[] { new Guid("938019c3-a144-4ea3-b702-9af8d0655201"), (sbyte)33, "Rundberg", "Rundbergsson" });

//        migrationBuilder.CreateIndex(
//            name: "IX_Mimbly_age",
//            table: "Mimbly",
//            column: "age");

//        migrationBuilder.CreateIndex(
//            name: "IX_refresh_token_refresh_token_user_id",
//            table: "refresh_token",
//            columns: new[] { "refresh_token", "user_id" });

//        migrationBuilder.CreateIndex(
//            name: "IX_refresh_token_user_id",
//            table: "refresh_token",
//            column: "user_id");

//        migrationBuilder.CreateIndex(
//            name: "IX_user_email",
//            table: "user",
//            column: "email",
//            unique: true);
//    }

//    protected override void Down(MigrationBuilder migrationBuilder)
//    {
//        migrationBuilder.DropTable(
//            name: "Mimbly");

//        migrationBuilder.DropTable(
//            name: "refresh_token");

//        migrationBuilder.DropTable(
//            name: "user");
//    }
//}