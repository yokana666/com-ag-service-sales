using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Update_Column_Name_Garment_Booking_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBookingPlan",
                table: "GarmentBookingOrders",
                newName: "IsBlokingPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlokingPlan",
                table: "GarmentBookingOrders",
                newName: "IsBookingPlan");
        }
    }
}
