using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financio.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Number = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Number);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Number", "Description", "Name" },
                values: new object[] { "0140", null, "Bank" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Number", "Description", "Name" },
                values: new object[] { "0740", null, "Eigenkapital" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
