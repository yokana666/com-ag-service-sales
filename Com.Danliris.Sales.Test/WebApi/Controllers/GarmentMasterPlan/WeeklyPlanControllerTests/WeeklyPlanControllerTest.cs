using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllerTests
{
    public class WeeklyPlanControllerTest : BaseControllerTest<WeeklyPlanController, WeeklyPlan, WeeklyPlanViewModel, IWeeklyPlanFacade>
    {
    }
}
