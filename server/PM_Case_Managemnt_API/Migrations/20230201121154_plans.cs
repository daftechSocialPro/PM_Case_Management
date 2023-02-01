using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class plans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StructureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodStartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodEndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectCordinatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanWeight = table.Column<float>(type: "real", nullable: false),
                    HasTask = table.Column<bool>(type: "bit", nullable: false),
                    PlandBudget = table.Column<float>(type: "real", nullable: false),
                    ProjectType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_BudgetYears_BudgetYearId",
                        column: x => x.BudgetYearId,
                        principalTable: "BudgetYears",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plans_OrganizationalStructures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "OrganizationalStructures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plans_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_BudgetYearId",
                table: "Plans",
                column: "BudgetYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ProgramId",
                table: "Plans",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_StructureId",
                table: "Plans",
                column: "StructureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
