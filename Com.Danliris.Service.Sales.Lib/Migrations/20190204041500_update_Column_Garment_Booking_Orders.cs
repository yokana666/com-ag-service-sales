using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class update_Column_Garment_Booking_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlokingPlan",
                table: "GarmentBookingOrders",
                newName: "IsBlockingPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlockingPlan",
                table: "GarmentBookingOrders",
                newName: "IsBlokingPlan");
        }
    }
}
