using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updatePOmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionOrder",
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
                    OrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    ShippingQuantityTolerance = table.Column<double>(nullable: false),
                    MaterialOrigin = table.Column<string>(maxLength: 255, nullable: true),
                    FinishWidth = table.Column<string>(maxLength: 255, nullable: true),
                    DesignNumber = table.Column<string>(maxLength: 255, nullable: true),
                    DesignCode = table.Column<string>(maxLength: 255, nullable: true),
                    HandlingStandard = table.Column<string>(maxLength: 255, nullable: true),
                    Run = table.Column<string>(maxLength: 255, nullable: true),
                    ShrinkageStandard = table.Column<string>(maxLength: 255, nullable: true),
                    ArticleFabricEdge = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 1000, nullable: true),
                    PackingInstruction = table.Column<string>(maxLength: 1000, nullable: true),
                    Sample = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    DistributedQuantity = table.Column<double>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    IsRequested = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    AutoIncreament = table.Column<long>(nullable: false),
                    SalesContractId = table.Column<long>(nullable: false),
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialId = table.Column<long>(nullable: false),
                    YarnMaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    YarnMaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ProcessTypeId = table.Column<long>(nullable: false),
                    ProcessTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProcessTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProcessTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    OrderTypeId = table.Column<long>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    OrderTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialPrice = table.Column<double>(nullable: false),
                    MaterialTags = table.Column<string>(maxLength: 255, nullable: true),
                    DesignMotiveID = table.Column<int>(nullable: false),
                    DesignMotiveCode = table.Column<string>(maxLength: 25, nullable: true),
                    DesignMotiveName = table.Column<string>(maxLength: 255, nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<long>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionRemark = table.Column<string>(nullable: true),
                    FinishTypeId = table.Column<long>(nullable: false),
                    FinishTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    FinishTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    FinishTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    StandardTestId = table.Column<long>(nullable: false),
                    StandardTestCode = table.Column<string>(maxLength: 255, nullable: true),
                    StandardTestName = table.Column<string>(maxLength: 1000, nullable: true),
                    StandardTestRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    AccountUserName = table.Column<string>(nullable: true),
                    ProfileFirstName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProfileLastName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProfileGender = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_Account_Roles",
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProductionOrder_Details",
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    ColorRequest = table.Column<string>(maxLength: 255, nullable: true),
                    ColorTemplate = table.Column<string>(maxLength: 255, nullable: true),
                    ColorTypeId = table.Column<string>(maxLength: 255, nullable: true),
                    ColorType = table.Column<string>(maxLength: 255, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_Details_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_LampStandard",
                columns: table => new
                {
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_LampStandard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_LampStandard_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_RunWidth",
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_RunWidth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_RunWidth_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_StandardTests",
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_StandardTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_StandardTests_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_Account_Roles_ProductionOrderModelId",
                table: "ProductionOrder_Account_Roles",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_Details_ProductionOrderModelId",
                table: "ProductionOrder_Details",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_LampStandard_ProductionOrderModelId",
                table: "ProductionOrder_LampStandard",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_RunWidth_ProductionOrderModelId",
                table: "ProductionOrder_RunWidth",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_StandardTests_ProductionOrderModelId",
                table: "ProductionOrder_StandardTests",
                column: "ProductionOrderModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionOrder_Account_Roles");

            migrationBuilder.DropTable(
                name: "ProductionOrder_Details");

            migrationBuilder.DropTable(
                name: "ProductionOrder_LampStandard");

            migrationBuilder.DropTable(
                name: "ProductionOrder_RunWidth");

            migrationBuilder.DropTable(
                name: "ProductionOrder_StandardTests");

            migrationBuilder.DropTable(
                name: "ProductionOrder");
        }
    }
}
