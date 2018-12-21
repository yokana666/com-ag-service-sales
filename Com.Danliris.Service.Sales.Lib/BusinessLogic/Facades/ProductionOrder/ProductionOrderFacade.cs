using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Danliris.Service.Sales.Lib.ViewModels.Report;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder
{
    public class ProductionOrderFacade : IProductionOrder
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<ProductionOrderModel> DbSet;
        private readonly IIdentityService identityService;
        private readonly ProductionOrderLogic productionOrderLogic;
        private readonly FinishingPrintingSalesContractLogic finishingPrintingSalesContractLogic;

        public ProductionOrderFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<ProductionOrderModel>();
            this.identityService = serviceProvider.GetService<IIdentityService>();
            this.productionOrderLogic = serviceProvider.GetService<ProductionOrderLogic>();
            this.finishingPrintingSalesContractLogic = serviceProvider.GetService<FinishingPrintingSalesContractLogic>();
        }

        public async Task<int> CreateAsync(ProductionOrderModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    List<object> productionOrderModelTemp = new List<object>();

                    List<ProductionOrder_DetailModel> DetailsTemp = new List<ProductionOrder_DetailModel>();
                    foreach (ProductionOrder_DetailModel dataTemp in model.Details)
                    {
                        DetailsTemp.Add(dataTemp);
                    }
                    ProductionOrderModel productionOrderModel = new ProductionOrderModel();
                    productionOrderModel = model;

                    int index = 0;

                    for (int i = 0; i < DetailsTemp.Count; i++)
                    {

                        List<ProductionOrder_RunWidthModel> runWidthTemp = new List<ProductionOrder_RunWidthModel>();
                        if (model.RunWidths.Count > 0)
                        {
                            foreach (ProductionOrder_RunWidthModel runWidth in model.RunWidths)
                            {
                                runWidthTemp.Add(runWidth);
                            }
                        }

                        productionOrderModel.RunWidths = runWidthTemp;


                        List<ProductionOrder_LampStandardModel> LampStandardsTemp = new List<ProductionOrder_LampStandardModel>();

                        foreach (ProductionOrder_LampStandardModel LampStandardModel in model.LampStandards)
                        {
                            LampStandardsTemp.Add(LampStandardModel);
                        }

                        productionOrderModel.LampStandards = LampStandardsTemp;

                        do
                        {
                            productionOrderModel.Code = CodeGenerator.Generate();
                        }
                        while (DbSet.Any(d => d.Code.Equals(productionOrderModel.Code)));

                        index += i;
                        ProductionOrderNumberGenerator(productionOrderModel, index);

                        var temp = productionOrderModel.Clone();

                        productionOrderLogic.Create(temp);
                    }

                    FinishingPrintingSalesContractModel dataFPSalesContract = await finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(productionOrderModel.SalesContractId));
                    if (dataFPSalesContract != null)
                    {
                        dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity - model.OrderQuantity;
                        this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    ProductionOrderModel model = await productionOrderLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        ProductionOrderModel productionOrderModel = new ProductionOrderModel();

                        productionOrderModel = model;
                        await productionOrderLogic.DeleteAsync(id);
                        FinishingPrintingSalesContractModel dataFPSalesContract = await this.finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(productionOrderModel.SalesContractId));
                        if (dataFPSalesContract != null)
                        {
                            dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity + model.OrderQuantity;
                            this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                        }
                    }
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<ProductionOrderModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return productionOrderLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<ProductionOrderModel> ReadByIdAsync(int id)
        {
            return await productionOrderLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, ProductionOrderModel model)
        {

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    productionOrderLogic.UpdateAsync(id, model);
                    FinishingPrintingSalesContractModel dataFPSalesContract = await this.finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(model.SalesContractId));
                    if (dataFPSalesContract != null)
                    {
                        dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity + model.OrderQuantity;
                        this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        private void ProductionOrderNumberGenerator(ProductionOrderModel model, int index)
        {
            ProductionOrderModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.OrderTypeName.Equals(model.OrderTypeName)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            string DocumentType = model.OrderTypeName.ToLower().Equals("printing") ? "P" : "F";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;

            if (lastData == null)
            {
                model.AutoIncreament = 1 + index;
                model.OrderNo = $"{DocumentType}/{YearNow}/0001";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.OrderNo = $"{DocumentType}/{YearNow}/0001";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.OrderNo = $"{DocumentType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(4, '0')}";
                }
            }
        }


        #region Report

        public IQueryable<DailyOperationViewModel> GetDailyOperationItems(string no)
        {
            
            List<DailyOperationViewModel> reportData = new List<DailyOperationViewModel>();
            try
            {
                string connectionString = APIEndpoint.ConnectionString;
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    
                    conn.Open();
                    if (string.IsNullOrEmpty(no))
                    {
                        using (SqlCommand cmd = new SqlCommand(
                        "select a.ProductionOrderOrderNo, b.Input  from Kanbans a join DailyOperation b on a.Id = b.KanbanId where a.IsDeleted=0 and b.IsDeleted=0", conn))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataSet dSet = new DataSet();
                            dataAdapter.Fill(dSet);
                            foreach (DataRow data in dSet.Tables[0].Rows)
                            {
                                DailyOperationViewModel view = new DailyOperationViewModel
                                {
                                    orderNo = data["ProductionOrderOrderNo"].ToString(),
                                    orderQuantity = string.IsNullOrWhiteSpace(data["Input"].ToString()) ? 0 : (double)data["Input"]
                                };
                                reportData.Add(view);
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand(
                        "select a.ProductionOrderOrderNo, b.Input, e.Name, a.SelectedProductionOrderDetailColorRequest, d.ProcessArea, d.Process "+
                        "from Kanbans a join DailyOperation b on a.Id = b.KanbanId "+
                        "join KanbanInstructions c on a.Id = c.KanbanId " +
                        "join KanbanSteps d on c.Id = d.InstructionId "+
                        "join Machine e on d.MachineId = e.Id "+
                        "where a.IsDeleted = 0 and b.IsDeleted = 0 and a.ProductionOrderOrderNo = '" + no + "'", conn))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataSet dSet = new DataSet();
                            dataAdapter.Fill(dSet);
                            foreach (DataRow data in dSet.Tables[0].Rows)
                            {
                                DailyOperationViewModel view = new DailyOperationViewModel
                                {
                                    orderNo = data["ProductionOrderOrderNo"].ToString(),
                                    orderQuantity = string.IsNullOrWhiteSpace(data["Input"].ToString()) ? 0 : (double)data["Input"],
                                    area= data["ProcessArea"].ToString(),
                                    color= data["SelectedProductionOrderDetailColorRequest"].ToString(),
                                    machine= data["Name"].ToString(),
                                    step= data["Process"].ToString()
                                };
                                reportData.Add(view);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                //Log exception
                //Display Error message
            }


            return reportData.AsQueryable();
        }

        public IQueryable<FabricQualityControlViewModel> GetFabricQualityItems(string no)
        {

            List<FabricQualityControlViewModel> reportData = new List<FabricQualityControlViewModel>();
            try
            {
                string connectionString = APIEndpoint.ConnectionString;
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {

                    conn.Open();
                    if (string.IsNullOrEmpty(no))
                    {
                        using (SqlCommand cmd = new SqlCommand(
                        "select OrderQuantity,ProductionOrderNo from FabricQualityControls where IsDeleted=0", conn))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataSet dSet = new DataSet();
                            dataAdapter.Fill(dSet);
                            foreach (DataRow data in dSet.Tables[0].Rows)
                            {
                                FabricQualityControlViewModel view = new FabricQualityControlViewModel
                                {
                                    orderNo = data["ProductionOrderNo"].ToString(),
                                    orderQuantity = string.IsNullOrWhiteSpace(data["OrderQuantity"].ToString()) ? 0 : (double)data["OrderQuantity"]
                                };
                                reportData.Add(view);
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand(
                        "select OrderQuantity,ProductionOrderNo,Grade from FabricQualityControls a join FabricGradeTests b on a.Id = b.FabricQualityControlId where a.IsDeleted = 0 and b.IsDeleted=0 and ProductionOrderNo='" + no +"'", conn))
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataSet dSet = new DataSet();
                            dataAdapter.Fill(dSet);
                            foreach (DataRow data in dSet.Tables[0].Rows)
                            {
                                FabricQualityControlViewModel view = new FabricQualityControlViewModel
                                {
                                    orderNo = data["ProductionOrderNo"].ToString(),
                                    orderQuantity = string.IsNullOrWhiteSpace(data["OrderQuantity"].ToString()) ? 0 : (double)data["OrderQuantity"],
                                    grade= data["Grade"].ToString()
                                };
                                reportData.Add(view);
                            }
                        }
                    }
                    
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                //Log exception
                //Display Error message
            }


            return reportData.AsQueryable();
        }
        public IQueryable<ProductionOrderReportViewModel> GetReportQuery(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = (from a in DbContext.ProductionOrder
                         join b in DbContext.ProductionOrder_Details on a.Id equals b.ProductionOrderModel.Id
                         where a.IsDeleted == false
                             && b.IsDeleted == false
                             && a.SalesContractNo == (string.IsNullOrWhiteSpace(salesContractNo) ? a.SalesContractNo : salesContractNo)
                             && a.OrderNo == (string.IsNullOrWhiteSpace(orderNo) ? a.OrderNo : orderNo)
                             && a.BuyerId.ToString() == (string.IsNullOrWhiteSpace(buyerId) ? a.BuyerId.ToString() : buyerId)
                             && a.OrderTypeId.ToString() == (string.IsNullOrWhiteSpace(orderTypeId) ? a.OrderTypeId.ToString() : orderTypeId)
                             && a.ProcessTypeId.ToString() == (string.IsNullOrWhiteSpace(processTypeId) ? a.ProcessTypeId.ToString() : processTypeId)
                             && a.AccountId.ToString() == (string.IsNullOrWhiteSpace(accountId) ? a.AccountId.ToString() : accountId)
                             && a.CreatedUtc.AddHours(offset).Date >= DateFrom.Date
                             && a.CreatedUtc.AddHours(offset).Date <= DateTo.Date
                         select new ProductionOrderReportViewModel
                         {
                             id=a.Id,
                             orderNo=a.OrderNo,
                             buyer=a.BuyerName,
                             buyerType=a.BuyerType,
                             colorRequest=b.ColorRequest,
                             orderQuantity = b.Quantity,
                             colorTemplate=b.ColorTemplate,
                             construction=a.MaterialConstructionName,
                             deliveryDate=a.DeliveryDate,
                             designCode=a.DesignCode,
                             orderType=a.OrderTypeName,
                             processType=a.ProcessTypeName,
                             staffName=a.CreatedBy,
                             _createdDate=a.CreatedUtc
                         });
            var dailyOP = GetDailyOperationItems(null).OrderByDescending(a=>a.orderNo);
            var fabricQuality = GetFabricQualityItems(null).OrderByDescending(a => a.orderNo);
            List<ProductionOrderReportViewModel> query = new List<ProductionOrderReportViewModel>();
            foreach (var data in Query)
            {
                double detailProd = 0;
                double detailPrep = 0;
                foreach(var daily in dailyOP)
                {
                    if (daily.orderNo == data.orderNo)
                    {
                        detailProd += daily.orderQuantity;
                    }
                }
                foreach (var prep in fabricQuality)
                {
                    if (prep.orderNo == data.orderNo)
                    {
                        detailPrep += prep.orderQuantity;
                    }
                }
                data.detail = data.orderQuantity + " di SPP \n" + detailProd + " di Produksi \n" + detailPrep + " di Pemeriksaan \n";
                if (detailPrep > 0)
                {
                    data.status = "Sudah dalam pemeriksaan kain";
                }
                else if (detailProd == 0)
                {
                    data.status = "Belum dalam Produksi";
                }
                else 
                {
                    data.status = "Sudah dalam Produksi";
                }
                query.Add(data);
            }

            return Query=query.AsQueryable();
        }

        public Tuple<List<ProductionOrderReportViewModel>, int> GetReport(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(salesContractNo, orderNo, orderTypeId, processTypeId, buyerId, accountId, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b._createdDate);
            }

            Pageable<ProductionOrderReportViewModel> pageable = new Pageable<ProductionOrderReportViewModel>(Query, page - 1, size);
            List<ProductionOrderReportViewModel> Data = pageable.Data.ToList<ProductionOrderReportViewModel>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData);
        }

        public MemoryStream GenerateExcel(string salesContractNo, string orderNo, string orderTypeId, string processTypeId, string buyerId, string accountId, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(salesContractNo, orderNo, orderTypeId, processTypeId, buyerId, accountId, dateFrom, dateTo, offset);
            Query = Query.OrderByDescending(b => b._createdDate);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Detail", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Panjang SPP (M)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Proses", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Hasil Matching", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "CW", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Staff Penjualan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Terima Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Permintaan Pengiriman", DataType = typeof(String) });

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", 0, "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Query)
                {
                    index++;
                    string deliverySchedule = item.deliveryDate == null ? "-" : item.deliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string createdDate = item._createdDate == null ? "-" : item._createdDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, item.status, item.detail, item.orderNo, item.orderQuantity, item.orderType, item.processType, item.construction,
                        item.designCode, item.colorTemplate, item.colorRequest, item.buyerType, item.staffName, createdDate, deliverySchedule);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }

        public ProductionOrderReportDetailViewModel GetDetailReport(long no)
        {
            var query = (from a in DbContext.ProductionOrder_Details
                         join b in DbContext.ProductionOrder on a.ProductionOrderModel.Id equals b.Id
                         where a.IsDeleted == false
                             && b.IsDeleted == false
                             && b.Id == no
                         select new ProductionOrderDetailViewModel
                         {
                             orderNo=b.OrderNo,
                             orderQuantity=a.Quantity,
                             uom=a.UomUnit
                         }).ToList();
            var sppNo = "";
            foreach(var spp in query)
            {
                sppNo = spp.orderNo;break;
            }
            ProductionOrderReportDetailViewModel detail = new ProductionOrderReportDetailViewModel();
            detail.SPPList = query;
            detail.QCList = GetFabricQualityItems(sppNo).ToList();
            detail.DailyOPList = GetDailyOperationItems(sppNo).ToList();
            return  detail;
        }

        #endregion
    }
}
