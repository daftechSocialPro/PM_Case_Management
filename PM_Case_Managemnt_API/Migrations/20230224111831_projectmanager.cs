using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class projectmanager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "ActivityProgresses");

            migrationBuilder.RenameColumn(
                name: "IsApprovedByCoordinator",
                table: "ActivityProgresses",
                newName: "IsApprovedByManager");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApprovedByManager",
                table: "ActivityProgresses",
                newName: "IsApprovedByCoordinator");

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "ActivityProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
