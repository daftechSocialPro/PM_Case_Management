using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShouldStat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShouldEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanedBudget = table.Column<float>(type: "real", nullable: false),
                    ActualStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualBudget = table.Column<float>(type: "real", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CommiteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Goal = table.Column<float>(type: "real", nullable: false),
                    Begining = table.Column<float>(type: "real", nullable: false),
                    ActualWorked = table.Column<float>(type: "real", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    OfficeWork = table.Column<float>(type: "real", nullable: false),
                    FieldWork = table.Column<float>(type: "real", nullable: false),
                    targetDivision = table.Column<int>(type: "int", nullable: true),
                    PostToCase = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActivityParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityParents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActivityParentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShouldStartPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActuallStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShouldEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanedBudget = table.Column<float>(type: "real", nullable: false),
                    ActualBudget = table.Column<float>(type: "real", nullable: true),
                    Goal = table.Column<float>(type: "real", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    ActualWorked = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HasActivity = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityParents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActualBudget = table.Column<float>(type: "real", nullable: false),
                    ActualWorked = table.Column<float>(type: "real", nullable: false),
                    EmployeeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quarterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinanceDocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isApprovedByCoordinator = table.Column<int>(type: "int", nullable: false),
                    isApprovedByFinance = table.Column<int>(type: "int", nullable: false),
                    isApprovedByDirector = table.Column<int>(type: "int", nullable: false),
                    FinanceApprovalRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatorApprovalRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorApprovalRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progressStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityProgresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTargetDivisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Target = table.Column<float>(type: "real", nullable: false),
                    TargetBudget = table.Column<float>(type: "real", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTargetDivisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTerminationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToCommiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TerminationReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByDirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isapproved = table.Column<bool>(type: "bit", nullable: false),
                    isRejected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTerminationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTerminationHistories_Employees_ApprovedByDirectorId",
                        column: x => x.ApprovedByDirectorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commitees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommiteeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commitees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskMemoReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskMemoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMemoReplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskMemos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActivityParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMemos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShouldStartPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActuallStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShouldEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanedBudget = table.Column<float>(type: "real", nullable: false),
                    ActualBudget = table.Column<float>(type: "real", nullable: true),
                    Goal = table.Column<float>(type: "real", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    ActualWorked = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HasActivityParent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeesAssignedForActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesAssignedForActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesAssignedForActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesAssignedForActivities_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityProgressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressAttachments_ActivityProgresses_ActivityProgressId",
                        column: x => x.ActivityProgressId,
                        principalTable: "ActivityProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommitesEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommiteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommiteeEmployeeStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitesEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitesEmployees_Commitees_CommiteeId",
                        column: x => x.CommiteeId,
                        principalTable: "Commitees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitesEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActivityParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMembers_ActivityParents_ActivityParentId",
                        column: x => x.ActivityParentId,
                        principalTable: "ActivityParents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskMembers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskMembers_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskMembers_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTerminationHistories_ApprovedByDirectorId",
                table: "ActivityTerminationHistories",
                column: "ApprovedByDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitesEmployees_CommiteeId",
                table: "CommitesEmployees",
                column: "CommiteeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitesEmployees_EmployeeId",
                table: "CommitesEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesAssignedForActivities_ActivityId",
                table: "EmployeesAssignedForActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesAssignedForActivities_EmployeeId",
                table: "EmployeesAssignedForActivities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAttachments_ActivityProgressId",
                table: "ProgressAttachments",
                column: "ActivityProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_ActivityParentId",
                table: "TaskMembers",
                column: "ActivityParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_EmployeeId",
                table: "TaskMembers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_PlanId",
                table: "TaskMembers",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMembers_TaskId",
                table: "TaskMembers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PlanId",
                table: "Tasks",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTargetDivisions");

            migrationBuilder.DropTable(
                name: "ActivityTerminationHistories");

            migrationBuilder.DropTable(
                name: "CommitesEmployees");

            migrationBuilder.DropTable(
                name: "EmployeesAssignedForActivities");

            migrationBuilder.DropTable(
                name: "ProgressAttachments");

            migrationBuilder.DropTable(
                name: "TaskMembers");

            migrationBuilder.DropTable(
                name: "TaskMemoReplies");

            migrationBuilder.DropTable(
                name: "TaskMemos");

            migrationBuilder.DropTable(
                name: "Commitees");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityProgresses");

            migrationBuilder.DropTable(
                name: "ActivityParents");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
