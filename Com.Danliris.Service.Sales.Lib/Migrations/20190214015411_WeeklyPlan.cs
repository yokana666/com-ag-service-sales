using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class WeeklyPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeeklyPlans",
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
                    Year = table.Column<short>(nullable: false),
                    UnitId = table.Column<long>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 255, nullable: true),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyPlanItems",
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
                    WeekNumber = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    Month = table.Column<byte>(nullable: false),
                    Efficiency = table.Column<double>(nullable: false),
                    Operator = table.Column<int>(nullable: false),
                    WorkingHours = table.Column<double>(nullable: false),
                    AHTotal = table.Column<double>(nullable: false),
                    EHTotal = table.Column<int>(nullable: false),
                    UsedEH = table.Column<int>(nullable: false),
                    RemainingEH = table.Column<int>(nullable: false),
                    WeeklyPlanId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyPlanItems_WeeklyPlans_WeeklyPlanId",
                        column: x => x.WeeklyPlanId,
                        principalTable: "WeeklyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyPlanItems_WeeklyPlanId",
                table: "WeeklyPlanItems",
                column: "WeeklyPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeklyPlanItems");

            migrationBuilder.DropTable(
                name: "WeeklyPlans");
        }
    }
}
