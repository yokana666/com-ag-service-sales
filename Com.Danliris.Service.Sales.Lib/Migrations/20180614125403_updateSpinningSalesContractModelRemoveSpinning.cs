using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updateSpinningSalesContractModelRemoveSpinning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialWidth",
                table: "SpinningSalesContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialWidth",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);
        }
    }
}
