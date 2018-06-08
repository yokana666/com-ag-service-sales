using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Danliris.Service.Sales.Lib
{
    public class SalesDbContext : StandardDbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }
    }
}
