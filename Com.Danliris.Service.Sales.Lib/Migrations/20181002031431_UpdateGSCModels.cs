using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateGSCModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "GarmentSalesContracts");

            migrationBuilder.RenameColumn(
                name: "Comodity",
                table: "GarmentSalesContracts",
                newName: "ComodityName");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "GarmentSalesContracts",
                newName: "BuyerBrandName");

            migrationBuilder.AlterColumn<int>(
                name: "ComodityId",
                table: "GarmentSalesContracts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerBrandCode",
                table: "GarmentSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerBrandId",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerBrandCode",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "BuyerBrandId",
                table: "GarmentSalesContracts");

            migrationBuilder.RenameColumn(
                name: "ComodityName",
                table: "GarmentSalesContracts",
                newName: "Comodity");

            migrationBuilder.RenameColumn(
                name: "BuyerBrandName",
                table: "GarmentSalesContracts",
                newName: "BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "ComodityId",
                table: "GarmentSalesContracts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true);
        }
    }
}
