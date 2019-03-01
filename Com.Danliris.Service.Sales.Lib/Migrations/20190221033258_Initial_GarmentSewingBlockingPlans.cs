using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Initial_GarmentSewingBlockingPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentSewingBlockingPlans",
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
                    BookingOrderNo = table.Column<string>(nullable: true),
                    BookingOrderDate = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSewingBlockingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSewingBlockingPlanItems",
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
                    BlockingPlanId = table.Column<long>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(nullable: true),
                    ComodityName = table.Column<string>(nullable: true),
                    SMVSewing = table.Column<double>(nullable: false),
                    WeeklyPlanId = table.Column<long>(nullable: false),
                    UnitId = table.Column<long>(nullable: false),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    Year = table.Column<short>(nullable: false),
                    WeeklyPlanItemId = table.Column<long>(nullable: false),
                    Weeknumber = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Efficiency = table.Column<double>(nullable: false),
                    EHBooking = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSewingBlockingPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentSewingBlockingPlanItems_GarmentSewingBlockingPlans_BlockingPlanId",
                        column: x => x.BlockingPlanId,
                        principalTable: "GarmentSewingBlockingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSewingBlockingPlanItems_BlockingPlanId",
                table: "GarmentSewingBlockingPlanItems",
                column: "BlockingPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentSewingBlockingPlanItems");

            migrationBuilder.DropTable(
                name: "GarmentSewingBlockingPlans");
        }
    }
}
