using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updateGSCName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSalesContractItem_GarmentSalesContract_GSCId",
                table: "GarmentSalesContractItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarmentSalesContractItem",
                table: "GarmentSalesContractItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarmentSalesContract",
                table: "GarmentSalesContract");

            migrationBuilder.RenameTable(
                name: "GarmentSalesContractItem",
                newName: "GarmentSalesContractItems");

            migrationBuilder.RenameTable(
                name: "GarmentSalesContract",
                newName: "GarmentSalesContracts");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSalesContractItem_GSCId",
                table: "GarmentSalesContractItems",
                newName: "IX_GarmentSalesContractItems_GSCId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarmentSalesContractItems",
                table: "GarmentSalesContractItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarmentSalesContracts",
                table: "GarmentSalesContracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContracts_GSCId",
                table: "GarmentSalesContractItems",
                column: "GSCId",
                principalTable: "GarmentSalesContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContracts_GSCId",
                table: "GarmentSalesContractItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarmentSalesContracts",
                table: "GarmentSalesContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarmentSalesContractItems",
                table: "GarmentSalesContractItems");

            migrationBuilder.RenameTable(
                name: "GarmentSalesContracts",
                newName: "GarmentSalesContract");

            migrationBuilder.RenameTable(
                name: "GarmentSalesContractItems",
                newName: "GarmentSalesContractItem");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSalesContractItems_GSCId",
                table: "GarmentSalesContractItem",
                newName: "IX_GarmentSalesContractItem_GSCId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarmentSalesContract",
                table: "GarmentSalesContract",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarmentSalesContractItem",
                table: "GarmentSalesContractItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSalesContractItem_GarmentSalesContract_GSCId",
                table: "GarmentSalesContractItem",
                column: "GSCId",
                principalTable: "GarmentSalesContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
