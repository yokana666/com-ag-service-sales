using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment
{
    public interface IRO_Garment_Validation
    {
        Task<int> ValidateROGarment(CostCalculationGarment model, Dictionary<long, string> productDicts);
    }
}
