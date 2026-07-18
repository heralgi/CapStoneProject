using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePolicyApi.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgentRemarks",
                table: "Claims",
                newName: "InternalStaffRemarks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternalStaffRemarks",
                table: "Claims",
                newName: "AgentRemarks");
        }
    }
}
