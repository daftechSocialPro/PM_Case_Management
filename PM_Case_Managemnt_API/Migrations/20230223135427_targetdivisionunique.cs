using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class targetdivisionunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isapproved",
                table: "ActivityTerminationHistories",
                newName: "Isapproved");

            migrationBuilder.RenameColumn(
                name: "isRejected",
                table: "ActivityTerminationHistories",
                newName: "IsRejected");

            migrationBuilder.RenameColumn(
                name: "quarterId",
                table: "ActivityProgresses",
                newName: "QuarterId");

            migrationBuilder.RenameColumn(
                name: "isApprovedByFinance",
                table: "ActivityProgresses",
                newName: "IsApprovedByFinance");

            migrationBuilder.RenameColumn(
                name: "isApprovedByDirector",
                table: "ActivityProgresses",
                newName: "IsApprovedByDirector");

            migrationBuilder.RenameColumn(
                name: "isApprovedByCoordinator",
                table: "ActivityProgresses",
                newName: "IsApprovedByCoordinator");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemos_ActivityParentId",
                table: "TaskMemos",
                column: "ActivityParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemos_PlanId",
                table: "TaskMemos",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemos_TaskId",
                table: "TaskMemos",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemoReplies_EmployeeId",
                table: "TaskMemoReplies",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMemoReplies_TaskMemoId",
                table: "TaskMemoReplies",
                column: "TaskMemoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTerminationHistories_ActivityId",
                table: "ActivityTerminationHistories",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTerminationHistories_FromEmployeeId",
                table: "ActivityTerminationHistories",
                column: "FromEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTerminationHistories_ToCommiteId",
                table: "ActivityTerminationHistories",
                column: "ToCommiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTerminationHistories_ToEmployeeId",
                table: "ActivityTerminationHistories",
                column: "ToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTargetDivisions_ActivityId",
                table: "ActivityTargetDivisions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProgresses_ActivityId",
                table: "ActivityProgresses",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProgresses_EmployeeValueId",
                table: "ActivityProgresses",
                column: "EmployeeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProgresses_QuarterId",
                table: "ActivityProgresses",
                column: "QuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityParents_TaskId",
                table: "ActivityParents",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityParentId",
                table: "Activities",
                column: "ActivityParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CommiteeId",
                table: "Activities",
                column: "CommiteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EmployeeId",
                table: "Activities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PlanId",
                table: "Activities",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TaskId",
                table: "Activities",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UnitOfMeasurementId",
                table: "Activities",
                column: "UnitOfMeasurementId");
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { }
                  
    }
}
