using Com.Danliris.Service.Sales.Lib.Models.Spinning;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib
{
    public class SalesDbContext : StandardDbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }


        public DbSet<WeavingSalesContractModel> WeavingSalesContract { get; set; }
        public DbSet<SpinningSalesContractModel> SpinningSalesContract { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
