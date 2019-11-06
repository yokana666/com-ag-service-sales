using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class BudgetJobOrderDisplayLogic : BaseMonitoringLogic<BudgetJobOrderDisplayViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public BudgetJobOrderDisplayLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<BudgetJobOrderDisplayViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var roNo = FilterDictionary["RONo"];
                Query = dbSet.Where(d => d.RO_Number == roNo);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(string.Concat("[RONo]", e.Message));
            }

            var result = Query.SelectMany(s => s.CostCalculationGarment_Materials.Select(m => new BudgetJobOrderDisplayViewModel
            {
                ProductCode = m.ProductCode,
                Description = m.Description,
                BudgetQuantity = m.BudgetQuantity, 
                UomPriceName = m.UOMPriceName,
                Price = m.Price,
                POSerialNumber = m.PO_SerialNumber
            }));

            return result;
        }
    }
}
