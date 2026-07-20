using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePolicyApi.Migrations
{
    /// <inheritdoc />
    public partial class Addednewcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadharNumber",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleNumber",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadharNumber",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "VehicleNumber",
                table: "Policies");
        }
    }
}
