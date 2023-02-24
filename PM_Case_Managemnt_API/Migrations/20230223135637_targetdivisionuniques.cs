using Microsoft.EntityFrameworkCore.Migrations;
using PM_Case_Managemnt_API.Models.PM;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    /// <inheritdoc />
    public partial class targetdivisionuniques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddUniqueConstraint( name:"target_division_uniquekey", table:"ActivityTargetDivisions",columns:new string[] { "ActivityId", "Order" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
