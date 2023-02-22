using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class noprojectcordinator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCordinatorId",
                table: "Plans");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_FinanceId",
                table: "Plans",
                column: "FinanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ProjectManagerId",
                table: "Plans",
                column: "ProjectManagerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Plans_Employees_FinanceId",
            //    table: "Plans",
            //    column: "FinanceId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Plans_Employees_ProjectManagerId",
            //    table: "Plans",
            //    column: "ProjectManagerId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Employees_FinanceId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Employees_ProjectManagerId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_FinanceId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_ProjectManagerId",
                table: "Plans");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectCordinatorId",
                table: "Plans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
