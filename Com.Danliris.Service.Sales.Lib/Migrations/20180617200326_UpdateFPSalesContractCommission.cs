using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateFPSalesContractCommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Commision",
                table: "FinishingPrintingSalesContracts",
                newName: "Commission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Commission",
                table: "FinishingPrintingSalesContracts",
                newName: "Commision");
        }
    }
}
