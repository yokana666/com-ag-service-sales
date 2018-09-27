using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddCostCalculationGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCalculationGarments",
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
                    Code = table.Column<string>(nullable: true),
                    RO_Number = table.Column<string>(nullable: true),
                    Article = table.Column<string>(nullable: true),
                    ComodityID = table.Column<string>(nullable: true),
                    ComodityCode = table.Column<string>(nullable: true),
                    Commodity = table.Column<string>(nullable: true),
                    CommodityDescription = table.Column<string>(nullable: true),
                    FabricAllowance = table.Column<double>(nullable: false),
                    AccessoriesAllowance = table.Column<double>(nullable: false),
                    Section = table.Column<string>(nullable: true),
                    UOMID = table.Column<string>(nullable: true),
                    UOMCode = table.Column<string>(nullable: true),
                    UOMUnit = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    SizeRange = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmDate = table.Column<DateTimeOffset>(nullable: false),
                    LeadTime = table.Column<int>(nullable: false),
                    SMV_Cutting = table.Column<double>(nullable: false),
                    SMV_Sewing = table.Column<double>(nullable: false),
                    SMV_Finishing = table.Column<double>(nullable: false),
                    SMV_Total = table.Column<double>(nullable: false),
                    BuyerId = table.Column<string>(nullable: true),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    EfficiencyId = table.Column<int>(nullable: false),
                    EfficiencyValue = table.Column<double>(nullable: false),
                    Index = table.Column<double>(nullable: false),
                    WageId = table.Column<int>(nullable: false),
                    WageRate = table.Column<double>(nullable: false),
                    THRId = table.Column<int>(nullable: false),
                    THRRate = table.Column<double>(nullable: false),
                    ConfirmPrice = table.Column<double>(nullable: false),
                    RateId = table.Column<int>(nullable: false),
                    RateValue = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    Insurance = table.Column<double>(nullable: false),
                    CommissionPortion = table.Column<double>(nullable: false),
                    CommissionRate = table.Column<double>(nullable: false),
                    OTL1Id = table.Column<int>(nullable: false),
                    OTL1Rate = table.Column<double>(nullable: false),
                    OTL1CalculatedRate = table.Column<double>(nullable: false),
                    OTL2Id = table.Column<int>(nullable: false),
                    OTL2Rate = table.Column<double>(nullable: false),
                    OTL2CalculatedRate = table.Column<double>(nullable: false),
                    Risk = table.Column<double>(nullable: false),
                    ProductionCost = table.Column<double>(nullable: false),
                    NETFOB = table.Column<double>(nullable: false),
                    FreightCost = table.Column<double>(nullable: false),
                    NETFOBP = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageFile = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RO_GarmentId = table.Column<int>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationGarments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCalculationGarment_Materials",
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
                    CostCalculationGarmentId = table.Column<int>(nullable: false),
                    CostCalculationGarmentId1 = table.Column<long>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    PO_SerialNumber = table.Column<string>(nullable: true),
                    PO = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    CategoryCode = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    Composition = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Yarn = table.Column<string>(nullable: true),
                    Width = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UOMQuantityId = table.Column<string>(nullable: true),
                    UOMQuantityName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    UOMPriceId = table.Column<string>(nullable: true),
                    UOMPriceName = table.Column<string>(nullable: true),
                    Conversion = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    isFabricCM = table.Column<bool>(nullable: false),
                    CM_Price = table.Column<double>(nullable: true),
                    ShippingFeePortion = table.Column<double>(nullable: false),
                    TotalShippingFee = table.Column<double>(nullable: false),
                    BudgetQuantity = table.Column<double>(nullable: false),
                    Information = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationGarment_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId1",
                        column: x => x.CostCalculationGarmentId1,
                        principalTable: "CostCalculationGarments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCalculationGarment_Materials");

            migrationBuilder.DropTable(
                name: "CostCalculationGarments");
        }
    }
}
