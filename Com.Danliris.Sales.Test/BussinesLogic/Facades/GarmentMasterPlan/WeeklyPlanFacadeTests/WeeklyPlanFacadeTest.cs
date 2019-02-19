using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.WeeklyPlanFacadeTests
{
    public class WeeklyPlanFacadeTest : BaseFacadeTest<SalesDbContext, WeeklyPlanFacade, WeeklyPlanLogic, GarmentWeeklyPlan, WeeklyPlanDataUtil>
    {
        private const string ENTITY = "GarmentWeeklyPlan";

        public WeeklyPlanFacadeTest() : base(ENTITY)
        {
        }
    }
}
