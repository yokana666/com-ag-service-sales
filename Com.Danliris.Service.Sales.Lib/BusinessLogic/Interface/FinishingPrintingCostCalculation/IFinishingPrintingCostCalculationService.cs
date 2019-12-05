using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation
{
    public interface IFinishingPrintingCostCalculationService : IBaseFacade<FinishingPrintingCostCalculationModel>
    {
        Task<bool> IsDataExistsById(int id);
    }
}
