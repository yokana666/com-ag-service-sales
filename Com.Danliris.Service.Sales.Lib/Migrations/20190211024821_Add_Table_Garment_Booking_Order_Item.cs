using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Add_Table_Garment_Booking_Order_Item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentBookingOrderItems",
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
                    BookingOrderId = table.Column<long>(nullable: false),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(nullable: true),
                    ComodityName = table.Column<string>(nullable: true),
                    ConfirmQuantity = table.Column<double>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmDate = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CanceledDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentBookingOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentBookingOrderItems_GarmentBookingOrders_BookingOrderId",
                        column: x => x.BookingOrderId,
                        principalTable: "GarmentBookingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentBookingOrderItems_BookingOrderId",
                table: "GarmentBookingOrderItems",
                column: "BookingOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentBookingOrderItems");
        }
    }
}
