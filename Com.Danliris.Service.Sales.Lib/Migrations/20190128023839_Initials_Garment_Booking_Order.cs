using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Initials_Garment_Booking_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentBookingOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    BookingOrderNo = table.Column<string>(nullable: true),
                    BookingOrderDate = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    SectionId = table.Column<long>(nullable: false),
                    SectionCode = table.Column<string>(nullable: true),
                    SectionName = table.Column<string>(nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsBookingPlan = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CanceledDate = table.Column<DateTimeOffset>(nullable: true),
                    CanceledQuantity = table.Column<double>(nullable: false),
                    ExpiredBookingDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpiredBookingQuantity = table.Column<double>(nullable: false),
                    ConfirmedQuantity = table.Column<double>(nullable: false),
                    HadConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentBookingOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentBookingOrders");
        }
    }
}
