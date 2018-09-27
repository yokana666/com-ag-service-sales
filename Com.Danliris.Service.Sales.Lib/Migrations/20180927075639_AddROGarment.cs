using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddROGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RO_Garments",
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
                    CostCalculationGarmentId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false),
                    ImagesPath = table.Column<string>(nullable: true),
                    ImagesName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garments_CostCalculationGarments_CostCalculationGarmentId",
                        column: x => x.CostCalculationGarmentId,
                        principalTable: "CostCalculationGarments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RO_Garment_SizeBreakdowns",
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
                    RO_GarmentId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: false),
                    ColorName = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garment_SizeBreakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garment_SizeBreakdowns_RO_Garments_RO_GarmentId",
                        column: x => x.RO_GarmentId,
                        principalTable: "RO_Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RO_Garment_SizeBreakdown_Details",
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
                    RO_Garment_SizeBreakdownId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garment_SizeBreakdown_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garment_SizeBreakdown_Details_RO_Garment_SizeBreakdowns_RO_Garment_SizeBreakdownId",
                        column: x => x.RO_Garment_SizeBreakdownId,
                        principalTable: "RO_Garment_SizeBreakdowns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garment_SizeBreakdown_Details_RO_Garment_SizeBreakdownId",
                table: "RO_Garment_SizeBreakdown_Details",
                column: "RO_Garment_SizeBreakdownId");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garment_SizeBreakdowns_RO_GarmentId",
                table: "RO_Garment_SizeBreakdowns",
                column: "RO_GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garments_CostCalculationGarmentId",
                table: "RO_Garments",
                column: "CostCalculationGarmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RO_Garment_SizeBreakdown_Details");

            migrationBuilder.DropTable(
                name: "RO_Garment_SizeBreakdowns");

            migrationBuilder.DropTable(
                name: "RO_Garments");
        }
    }
}
