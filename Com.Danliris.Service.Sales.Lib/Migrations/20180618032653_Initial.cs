using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishingPrintingSalesContracts",
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
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankAccountName = table.Column<string>(nullable: true),
                    AccountBankID = table.Column<int>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankNumber = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankCurrencyID = table.Column<int>(maxLength: 255, nullable: false),
                    AccountBankCurrencyCode = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankCurrencySymbol = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankCurrencyRate = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AgentID = table.Column<int>(nullable: false),
                    AgentCode = table.Column<string>(maxLength: 25, nullable: true),
                    AgentName = table.Column<string>(maxLength: 255, nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerID = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 25, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    Commission = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityID = table.Column<int>(nullable: false),
                    CommodityCode = table.Column<string>(maxLength: 25, nullable: true),
                    CommodityName = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliverySchedule = table.Column<DateTimeOffset>(nullable: false),
                    DesignMotiveID = table.Column<int>(nullable: false),
                    DesignMotiveCode = table.Column<string>(maxLength: 25, nullable: true),
                    DesignMotiveName = table.Column<string>(maxLength: 255, nullable: true),
                    DispositionNumber = table.Column<string>(maxLength: 255, nullable: true),
                    FromStock = table.Column<bool>(nullable: false),
                    MaterialID = table.Column<int>(nullable: false),
                    MaterialCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialName = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialConstructionName = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    OrderTypeID = table.Column<int>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 25, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    PieceLength = table.Column<string>(maxLength: 255, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<int>(nullable: false),
                    QualityID = table.Column<int>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 25, nullable: true),
                    QualityName = table.Column<string>(maxLength: 255, nullable: true),
                    SalesContractNo = table.Column<string>(maxLength: 25, nullable: true),
                    ShipmentDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    ShippingQuantityTolerance = table.Column<double>(maxLength: 1000, nullable: false),
                    TermOfPaymentID = table.Column<int>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 25, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    TermOfShipment = table.Column<string>(maxLength: 1000, nullable: true),
                    TransportFee = table.Column<string>(maxLength: 1000, nullable: true),
                    UseIncomeTax = table.Column<bool>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialID = table.Column<int>(nullable: false),
                    YarnMaterialCode = table.Column<string>(maxLength: 25, nullable: true),
                    YarnMaterialName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingSalesContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpinningSalesContract",
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
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 1000, nullable: true),
                    ComodityType = table.Column<string>(maxLength: 255, nullable: true),
                    QualityId = table.Column<long>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 255, nullable: true),
                    QualityName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentId = table.Column<long>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountBankNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyId = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<long>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 1000, nullable: true),
                    AgentCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinningSalesContract", x => x.Id);
                });

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
                    AutoIncrementNumber = table.Column<int>(nullable: false),
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
                    TermOfPaymentId = table.Column<long>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountBankNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyId = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<long>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 1000, nullable: true),
                    AgentCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeavingSalesContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishingPrintingSalesContractDetails",
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
                    FinishingPrintingSalesContractId = table.Column<long>(nullable: true),
                    Color = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyID = table.Column<int>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 25, nullable: true),
                    CurrencySymbol = table.Column<string>(maxLength: 25, nullable: true),
                    CurrencyRate = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    UseIncomeTax = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingSalesContractDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishingPrintingSalesContractDetails_FinishingPrintingSalesContracts_FinishingPrintingSalesContractId",
                        column: x => x.FinishingPrintingSalesContractId,
                        principalTable: "FinishingPrintingSalesContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContractDetails_FinishingPrintingSalesContractId",
                table: "FinishingPrintingSalesContractDetails",
                column: "FinishingPrintingSalesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[SalesContractNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishingPrintingSalesContractDetails");

            migrationBuilder.DropTable(
                name: "SpinningSalesContract");

            migrationBuilder.DropTable(
                name: "WeavingSalesContract");

            migrationBuilder.DropTable(
                name: "FinishingPrintingSalesContracts");
        }
    }
}
