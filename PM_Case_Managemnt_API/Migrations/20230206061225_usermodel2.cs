using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.Authentication
{
    /// <inheritdoc />
    public partial class usermodel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "AspNetUsers",
                newName: "EmployeesId");

            //migrationBuilder.CreateTable(
            //    name: "Employee",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Gender = table.Column<int>(type: "int", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        RowStatus = table.Column<int>(type: "int", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Employee", x => x.Id);
            //    });
        

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeesId",
                table: "AspNetUsers",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employee_EmployeesId",
                table: "AspNetUsers",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employee_EmployeesId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeesId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "AspNetUsers",
                newName: "EmployeeId");
        }
    }
}
