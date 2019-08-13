using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic
{
    public interface ICostCalculationGarment : IBaseFacade<CostCalculationGarment>
    {
        Task<Dictionary<long, string>> GetProductNames(List<long> productIds);
        ReadResponse<CostCalculationGarment> ReadForROAcceptance(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> AcceptanceCC(List<long> listId, string user);
    }
}
