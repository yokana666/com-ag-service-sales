using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class initialGarmentSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionOrder_Account_Roles");

            migrationBuilder.CreateTable(
                name: "GarmentSalesContract",
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
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    CostCalculationId = table.Column<int>(nullable: false),
                    ROId = table.Column<int>(nullable: false),
                    RONumber = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerId = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(maxLength: 500, nullable: true),
                    ComodityId = table.Column<string>(nullable: true),
                    Comodity = table.Column<string>(maxLength: 500, nullable: true),
                    ComodityCode = table.Column<string>(maxLength: 500, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    Article = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    Material = table.Column<string>(maxLength: 3000, nullable: true),
                    DocPresented = table.Column<string>(maxLength: 3000, nullable: true),
                    FOB = table.Column<string>(maxLength: 3000, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Delivery = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: true),
                    NoHS = table.Column<string>(maxLength: 3000, nullable: true),
                    IsMaterial = table.Column<bool>(nullable: false),
                    IsTrimming = table.Column<bool>(nullable: false),
                    IsEmbrodiary = table.Column<bool>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: false),
                    IsWash = table.Column<bool>(nullable: false),
                    IsTTPayment = table.Column<bool>(nullable: false),
                    PaymentDetail = table.Column<string>(nullable: true),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankName = table.Column<string>(maxLength: 500, nullable: true),
                    AccountName = table.Column<string>(maxLength: 500, nullable: true),
                    DocPrinted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSalesContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSalesContractItem",
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
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    GSCId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSalesContractItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentSalesContractItem_GarmentSalesContract_GSCId",
                        column: x => x.GSCId,
                        principalTable: "GarmentSalesContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSalesContractItem_GSCId",
                table: "GarmentSalesContractItem",
                column: "GSCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentSalesContractItem");

            migrationBuilder.DropTable(
                name: "GarmentSalesContract");

            migrationBuilder.CreateTable(
                name: "ProductionOrder_Account_Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    UId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_Account_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_Account_Roles_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_Account_Roles_ProductionOrderModelId",
                table: "ProductionOrder_Account_Roles",
                column: "ProductionOrderModelId");
        }
    }
}
