using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updateSpinningSalesContractModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialConstructionCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionRemark",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "YarnMaterialCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "YarnMaterialId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "YarnMaterialName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "YarnMaterialRemark",
                table: "SpinningSalesContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionCode",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaterialConstructionId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "SpinningSalesContract",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionRemark",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialCode",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "YarnMaterialId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialName",
                table: "SpinningSalesContract",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialRemark",
                table: "SpinningSalesContract",
                nullable: true);
        }
    }
}
