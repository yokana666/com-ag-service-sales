using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateCostCalculationGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.AddColumn<string>(
                name: "BuyerBrandCode",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerBrandId",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BuyerBrandName",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId",
                principalTable: "CostCalculationGarments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "BuyerBrandCode",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "BuyerBrandId",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "BuyerBrandName",
                table: "CostCalculationGarments");

            migrationBuilder.AlterColumn<int>(
                name: "CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId1",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId1",
                principalTable: "CostCalculationGarments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
