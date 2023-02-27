using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class filesInfochange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AppointemnetWithCalenders");

            migrationBuilder.DropColumn(
                name: "FileLookup",
                table: "FilesInformations");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "FilesInformations",
                newName: "CaseId");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "FilesInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileDescription",
                table: "FilesInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.CreateTable(
            //    name: "AppointementWithCalender",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        AppointementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        RowStatus = table.Column<int>(type: "int", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppointementWithCalender", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AppointementWithCalender_Cases_CaseId",
            //            column: x => x.CaseId,
            //            principalTable: "Cases",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AppointementWithCalender_Employees_EmployeeId",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employees",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_FilesInformations_CaseId",
                table: "FilesInformations",
                column: "CaseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppointementWithCalender_CaseId",
            //    table: "AppointementWithCalender",
            //    column: "CaseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppointementWithCalender_EmployeeId",
            //    table: "AppointementWithCalender",
            //    column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilesInformations_Cases_CaseId",
                table: "FilesInformations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesInformations_Cases_CaseId",
                table: "FilesInformations");

            migrationBuilder.DropTable(
                name: "AppointementWithCalender");

            migrationBuilder.DropIndex(
                name: "IX_FilesInformations_CaseId",
                table: "FilesInformations");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "FilesInformations",
                newName: "ParentId");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "FilesInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileDescription",
                table: "FilesInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileLookup",
                table: "FilesInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppointemnetWithCalenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointemnetWithCalenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointemnetWithCalenders_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointemnetWithCalenders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointemnetWithCalenders_CaseId",
                table: "AppointemnetWithCalenders",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointemnetWithCalenders_EmployeeId",
                table: "AppointemnetWithCalenders",
                column: "EmployeeId");
        }
    }
}
