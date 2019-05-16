using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Danliris.Service.Sales.Lib.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder
{
    public interface IProductionOrder : IBaseFacade<ProductionOrderModel>
    {
        Tuple<List<ProductionOrderReportViewModel>, int> GetReport(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset);
        ProductionOrderReportDetailViewModel GetDetailReport(long no);
        Task<int> UpdateRequestedTrue(List<int> ids);
        Task<int> UpdateRequestedFalse(List<int> ids);
        Task<int> UpdateIsCompletedTrue(int id);
        Task<int> UpdateIsCompletedFalse(int id);
        Task<int> UpdateDistributedQuantity(List<int> id, List<double> distributedQuantity);
    }
}
