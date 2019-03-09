using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class add_Status_SewingBlockingPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GarmentSewingBlockingPlanItems");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GarmentSewingBlockingPlans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GarmentSewingBlockingPlans");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GarmentSewingBlockingPlanItems",
                nullable: true);
        }
    }
}
