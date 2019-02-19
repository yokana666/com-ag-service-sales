using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils
{
    public class WeeklyPlanDataUtil : BaseDataUtil<WeeklyPlanFacade, GarmentWeeklyPlan>
    {
        public WeeklyPlanDataUtil(WeeklyPlanFacade facade) : base(facade)
        {
        }

        public override GarmentWeeklyPlan GetNewData()
        {
            var weeklyPlan = new GarmentWeeklyPlan
            {
                Year = 2019,
                UnitId = 1,
                UnitCode = "UnitCode",
                UnitName = "UnitName",
                Items = new List<GarmentWeeklyPlanItem>()
            };
            var startDateOfYear = new DateTime(2019, 01, 01);
            var endDateOfYear = new DateTime(2019, 12, 31);
            var diff = (endDateOfYear - startDateOfYear).TotalMilliseconds;
            var totalWeek = Math.Ceiling((decimal)((diff / 86400000) + 1) / 7);
            for (byte i = 1; i <= totalWeek; i++)
            {
                weeklyPlan.Items.Add(new GarmentWeeklyPlanItem
                {
                    WeekNumber = (byte)(i + 1)
                });
            }
            return weeklyPlan;
        }
    }
}
