using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updateSpinningSalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTags",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "UomId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "UomUnit",
                table: "SpinningSalesContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SpinningSalesContract",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTags",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UomId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UomUnit",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);
        }
    }
}
