using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";
        
        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

    }
}
