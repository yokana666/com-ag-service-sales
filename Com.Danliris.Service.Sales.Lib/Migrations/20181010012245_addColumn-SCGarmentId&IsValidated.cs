using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class addColumnSCGarmentIdIsValidated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SCGarmentId",
                table: "CostCalculationGarments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "SCGarmentId",
                table: "CostCalculationGarments");
        }
    }
}
