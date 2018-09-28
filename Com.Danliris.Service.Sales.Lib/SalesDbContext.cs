using Com.Danliris.Service.Sales.Lib.Models.Spinning;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.Models;

namespace Com.Danliris.Service.Sales.Lib
{
    public class SalesDbContext : StandardDbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<WeavingSalesContractModel> WeavingSalesContract { get; set; }
        public DbSet<SpinningSalesContractModel> SpinningSalesContract { get; set; }
        public DbSet<FinishingPrintingSalesContractModel> FinishingPrintingSalesContracts { get; set; }
        public DbSet<FinishingPrintingSalesContractDetailModel> FinishingPrintingSalesContractDetails { get; set; }
		public DbSet<CostCalculationGarment> CostCalculationGarments { get; set; }
		public DbSet<CostCalculationGarment_Material> CostCalculationGarment_Materials { get; set; }
        public DbSet<GarmentSalesContract> GarmentSalesContracts { get; set; }
        public DbSet<GarmentSalesContractItem> GarmentSalesContractItems { get; set; }

        #region PRODUCTION ORDER DBSET
        public DbSet<ProductionOrderModel> ProductionOrder { get; set; }
        public DbSet<ProductionOrder_DetailModel> ProductionOrder_Details { get; set; }
        public DbSet<ProductionOrder_LampStandardModel> ProductionOrder_LampStandard { get; set; }
        public DbSet<ProductionOrder_RunWidthModel> ProductionOrder_RunWidth { get; set; }

        #endregion

        public DbSet<RO_Garment> RO_Garments { get; set; }
        public DbSet<RO_Garment_SizeBreakdown> RO_Garment_SizeBreakdowns { get; set; }
        public DbSet<RO_Garment_SizeBreakdown_Detail> RO_Garment_SizeBreakdown_Details { get; set; }

        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<FinishingPrintingSalesContractModel>()
                .HasIndex(h => h.SalesContractNo)
                .IsUnique();

            modelBuilder.Entity<RO_Garment>()
            .Ignore(c => c.ImagesFile);
        }
    }
}
