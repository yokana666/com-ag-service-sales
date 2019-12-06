using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";

        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public void ValidateVM()
        {
            var vm = new FinishingPrintingCostCalculationViewModel();
            var response = vm.Validate(null);
            Assert.NotEmpty(response);
        }
    }
}
