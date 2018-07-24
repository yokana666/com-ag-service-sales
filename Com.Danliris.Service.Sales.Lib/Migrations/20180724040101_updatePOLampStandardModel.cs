using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updatePOLampStandardModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProductionOrderModelId",
                table: "ProductionOrder_LampStandard",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "LampStandardId",
                table: "ProductionOrder_LampStandard",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ProductionOrderModelId",
                table: "ProductionOrder_Details",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LampStandardId",
                table: "ProductionOrder_LampStandard");

            migrationBuilder.AlterColumn<long>(
                name: "ProductionOrderModelId",
                table: "ProductionOrder_LampStandard",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductionOrderModelId",
                table: "ProductionOrder_Details",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
