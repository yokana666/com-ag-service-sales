using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class updateModelWeavingSalesContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TermPaymentName",
                table: "WeavingSalesContract",
                newName: "TermOfPaymentName");

            migrationBuilder.RenameColumn(
                name: "TermPaymentIsExport",
                table: "WeavingSalesContract",
                newName: "TermOfPaymentIsExport");

            migrationBuilder.RenameColumn(
                name: "TermPaymentId",
                table: "WeavingSalesContract",
                newName: "TermOfPaymentId");

            migrationBuilder.RenameColumn(
                name: "TermPaymentCode",
                table: "WeavingSalesContract",
                newName: "TermOfPaymentCode");

            migrationBuilder.AlterColumn<string>(
                name: "AccountBankNumber",
                table: "WeavingSalesContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AccountCurrencyCode",
                table: "WeavingSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountCurrencyId",
                table: "WeavingSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutoIncrementNumber",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCurrencyCode",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "AccountCurrencyId",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "AutoIncrementNumber",
                table: "WeavingSalesContract");

            migrationBuilder.RenameColumn(
                name: "TermOfPaymentName",
                table: "WeavingSalesContract",
                newName: "TermPaymentName");

            migrationBuilder.RenameColumn(
                name: "TermOfPaymentIsExport",
                table: "WeavingSalesContract",
                newName: "TermPaymentIsExport");

            migrationBuilder.RenameColumn(
                name: "TermOfPaymentId",
                table: "WeavingSalesContract",
                newName: "TermPaymentId");

            migrationBuilder.RenameColumn(
                name: "TermOfPaymentCode",
                table: "WeavingSalesContract",
                newName: "TermPaymentCode");

            migrationBuilder.AlterColumn<int>(
                name: "AccountBankNumber",
                table: "WeavingSalesContract",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
