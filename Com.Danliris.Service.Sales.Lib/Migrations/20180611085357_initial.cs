using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeavingSalesContract",
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
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    DispositionNumber = table.Column<string>(maxLength: 255, nullable: true),
                    FromStock = table.Column<bool>(nullable: false),
                    MaterialWidth = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    ShippingQuantityTolerance = table.Column<double>(nullable: false),
                    ComodityDescription = table.Column<string>(nullable: true),
                    IncomeTax = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfShipment = table.Column<string>(maxLength: 1000, nullable: true),
                    TransportFee = table.Column<string>(maxLength: 1000, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 1000, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Comission = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliverySchedule = table.Column<DateTimeOffset>(nullable: false),
                    ShipmentDescription = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    PieceLength = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProductPrice = table.Column<double>(nullable: false),
                    ProductTags = table.Column<string>(maxLength: 255, nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<long>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionRemark = table.Column<string>(nullable: true),
                    YarnMaterialId = table.Column<long>(nullable: false),
                    YarnMaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    YarnMaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialRemark = table.Column<string>(nullable: true),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 1000, nullable: true),
                    ComodityType = table.Column<string>(maxLength: 255, nullable: true),
                    QualityId = table.Column<long>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 255, nullable: true),
                    QualityName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermPaymentId = table.Column<long>(nullable: false),
                    TermPaymentCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermPaymentName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermPaymentIsExport = table.Column<bool>(nullable: false),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountBankNumber = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<long>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 1000, nullable: true),
                    AgentCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeavingSalesContract", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeavingSalesContract");
        }
    }
}
