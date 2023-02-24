using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class removeemployeeidfromforwards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseForwards_Employees_EmployeeId",
                table: "CaseForwards");

            migrationBuilder.DropIndex(
                name: "IX_CaseForwards_EmployeeId",
                table: "CaseForwards");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CaseForwards");

            migrationBuilder.CreateIndex(
                name: "IX_CaseForwards_ForwardedByEmployeeId",
                table: "CaseForwards",
                column: "ForwardedByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseForwards_Employees_ForwardedByEmployeeId",
                table: "CaseForwards",
                column: "ForwardedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseForwards_Employees_ForwardedByEmployeeId",
                table: "CaseForwards");

            migrationBuilder.DropIndex(
                name: "IX_CaseForwards_ForwardedByEmployeeId",
                table: "CaseForwards");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "CaseForwards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CaseForwards_EmployeeId",
                table: "CaseForwards",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseForwards_Employees_EmployeeId",
                table: "CaseForwards",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
