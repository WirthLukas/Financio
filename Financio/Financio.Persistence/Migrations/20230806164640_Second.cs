using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financio.Persistence.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Number = table.Column<string>(type: "nchar(4)", fixedLength: true, maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nchar(4)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountAccountReference",
                columns: table => new
                {
                    CounterAccountReferencesId = table.Column<int>(type: "int", nullable: false),
                    CounterAccountsNumber = table.Column<string>(type: "nchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAccountReference", x => new { x.CounterAccountReferencesId, x.CounterAccountsNumber });
                    table.ForeignKey(
                        name: "FK_AccountAccountReference_Accounts_CounterAccountsNumber",
                        column: x => x.CounterAccountsNumber,
                        principalTable: "Accounts",
                        principalColumn: "Number");
                    table.ForeignKey(
                        name: "FK_AccountAccountReference_References_CounterAccountReferencesId",
                        column: x => x.CounterAccountReferencesId,
                        principalTable: "References",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAccountReference_CounterAccountsNumber",
                table: "AccountAccountReference",
                column: "CounterAccountsNumber");

            migrationBuilder.CreateIndex(
                name: "IX_References_AccountNumber",
                table: "References",
                column: "AccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAccountReference");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
