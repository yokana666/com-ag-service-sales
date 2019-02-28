using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Update_WeekNumber_GarmentSewingBlockingPlanItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weeknumber",
                table: "GarmentSewingBlockingPlanItems",
                newName: "WeekNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeekNumber",
                table: "GarmentSewingBlockingPlanItems",
                newName: "Weeknumber");
        }
    }
}
