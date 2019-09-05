using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Microsoft.AspNetCore.JsonPatch;
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
        ReadResponse<CostCalculationGarment> ReadForROAvailable(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> AvailableCC(List<long> listId, string user);
        ReadResponse<CostCalculationGarment> ReadForRODistribution(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> DistributeCC(List<long> listId, string user);
        Task<int> PostCC(List<long> listId);
        Task<int> UnpostCC(long id, string reason);
        Task<int> Patch(long id, JsonPatchDocument<CostCalculationGarment> jsonPatch);
    }
}
