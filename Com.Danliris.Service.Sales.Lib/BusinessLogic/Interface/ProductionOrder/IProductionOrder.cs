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
        Task<Tuple<List<ProductionOrderReportViewModel>, int>> GetReport(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        Task<MemoryStream> GenerateExcel(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<ProductionOrderReportDetailViewModel> GetDetailReport(long no);
    }
}
