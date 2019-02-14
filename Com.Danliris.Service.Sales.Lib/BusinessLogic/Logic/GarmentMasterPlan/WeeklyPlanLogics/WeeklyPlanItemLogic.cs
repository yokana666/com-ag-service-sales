using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics
{
    public class WeeklyPlanItemLogic : BaseLogic<WeeklyPlanItem>
    {
        public WeeklyPlanItemLogic(IIdentityService IdentityService, IServiceProvider serviceProvider, SalesDbContext dbContext) : base(IdentityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<WeeklyPlanItem> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }
    }
}
