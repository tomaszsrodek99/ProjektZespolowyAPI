using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPI.Migrations
{
    public partial class PrivateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PrivateCategory_PrivateCategoryId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCategory",
                table: "PrivateCategory");

            migrationBuilder.RenameTable(
                name: "PrivateCategory",
                newName: "PrivateCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCategories",
                table: "PrivateCategories",
                column: "PrivateCategoryId");

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BudgetLimit = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BudgetSpent = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PrivateCategories_PrivateCategoryId",
                table: "Expenses",
                column: "PrivateCategoryId",
                principalTable: "PrivateCategories",
                principalColumn: "PrivateCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PrivateCategories_PrivateCategoryId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCategories",
                table: "PrivateCategories");

            migrationBuilder.RenameTable(
                name: "PrivateCategories",
                newName: "PrivateCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCategory",
                table: "PrivateCategory",
                column: "PrivateCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PrivateCategory_PrivateCategoryId",
                table: "Expenses",
                column: "PrivateCategoryId",
                principalTable: "PrivateCategory",
                principalColumn: "PrivateCategoryId");
        }
    }
}
