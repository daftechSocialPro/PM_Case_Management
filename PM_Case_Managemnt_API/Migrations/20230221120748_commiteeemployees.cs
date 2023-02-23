using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class commiteeemployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitesEmployees_Commitees_CommiteeId",
                table: "CommitesEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitesEmployees_Employees_EmployeeId",
                table: "CommitesEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitesEmployees",
                table: "CommitesEmployees");

            migrationBuilder.RenameTable(
                name: "CommitesEmployees",
                newName: "CommiteEmployees");

            //migrationBuilder.RenameIndex(
            //    name: "IX_CommitesEmployees_EmployeeId",
            //    table: "CommiteEmployees",
            //    newName: "IX_CommiteEmployees_EmployeeId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_CommitesEmployees_CommiteeId",
            //    table: "CommiteEmployees",
            //    newName: "IX_CommiteEmployees_CommiteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommiteEmployees",
                table: "CommiteEmployees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemos_EmployeeId",
                table: "TaskMemos",
                column: "EmployeeId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CommiteEmployees_Commitees_CommiteeId",
            //    table: "CommiteEmployees",
            //    column: "CommiteeId",
            //    principalTable: "Commitees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CommiteEmployees_Employees_EmployeeId",
            //    table: "CommiteEmployees",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskMemos_Employees_EmployeeId",
            //    table: "TaskMemos",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommiteEmployees_Commitees_CommiteeId",
                table: "CommiteEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CommiteEmployees_Employees_EmployeeId",
                table: "CommiteEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskMemos_Employees_EmployeeId",
                table: "TaskMemos");

            migrationBuilder.DropIndex(
                name: "IX_TaskMemos_EmployeeId",
                table: "TaskMemos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommiteEmployees",
                table: "CommiteEmployees");

            migrationBuilder.RenameTable(
                name: "CommiteEmployees",
                newName: "CommitesEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_CommiteEmployees_EmployeeId",
                table: "CommitesEmployees",
                newName: "IX_CommitesEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommiteEmployees_CommiteeId",
                table: "CommitesEmployees",
                newName: "IX_CommitesEmployees_CommiteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitesEmployees",
                table: "CommitesEmployees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitesEmployees_Commitees_CommiteeId",
                table: "CommitesEmployees",
                column: "CommiteeId",
                principalTable: "Commitees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitesEmployees_Employees_EmployeeId",
                table: "CommitesEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
