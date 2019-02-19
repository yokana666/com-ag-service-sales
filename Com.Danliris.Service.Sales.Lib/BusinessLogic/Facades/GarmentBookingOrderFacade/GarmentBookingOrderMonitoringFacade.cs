using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade
{
    public class GarmentBookingOrderMonitoringFacade //: IGarmentBookingOrderMonitoringInterface
    {
        //private readonly SalesDbContext DbContext;
        //private readonly DbSet<GarmentBookingOrder> DbSet;
        //private IdentityService IdentityService;
        //private GarmentBookingOrderLogic GarmentBookingOrderLogic;

        //public GarmentBookingOrderMonitoringFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        //{
        //    this.DbContext = dbContext;
        //    this.DbSet = this.DbContext.Set<GarmentBookingOrder>(); ;
        //    this.IdentityService = serviceProvider.GetService<IdentityService>();
        //    this.GarmentBookingOrderLogic = serviceProvider.GetService<GarmentBookingOrderLogic>(); ;
        //}

        //public IQueryable<GarmentBookingOrderMonitoringViewModel> GetReportQuery(string no, string buyerCode, string sectionName, DateTime? dateFrom, DateTime? dateTo, int offset)
        //{
        //    DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
        //    DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

        //    var Query = (from a in DbContext.GarmentBookingOrders
        //                 join b in DbContext.GarmentBookingOrderItems on a.Id equals b.GarmentBookingOrder.Id

        //                 where a.IsDeleted == false
        //                     && b.IsDeleted == false
        //                     && a.BookingOrderNo == (string.IsNullOrWhiteSpace(no) ? a.BookingOrderNo : no)
        //                     && a.BuyerName == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
        //                     && a.SectionName == (string.IsNullOrWhiteSpace(sectionName) ? a.SectionName : sectionName)
        //                     && a.CreatedUtc.AddHours(offset).Date >= DateFrom.Date
        //                     && a.CreatedUtc.AddHours(offset).Date <= DateTo.Date
        //                 select new GarmentBookingOrderMonitoringViewModel
        //                 {
        //                     CreatedUtc = a.CreatedUtc,
        //                     BookingOrderNo = a.BookingOrderNo,
        //                     BookingOrderDate = a.BookingOrderDate,
        //                     ComodityName = b.ComodityName,
        //                     BuyerName = a.BuyerName,
        //                     DeliveryDate = a.DeliveryDate,
        //                     OrderQuantity = a.OrderQuantity,
        //                     DeliveryDateItems = b.DeliveryDate,
        //                     ConfirmDate = b.ConfirmDate,
        //                     ConfirmQuantity = b.ConfirmQuantity,
        //                     Remark = b.Remark,
        //                 });
        //    return Query;
        //}

        //public Tuple<List<GarmentBookingOrderMonitoringViewModel>, int> GetReport(string no, string buyerCode, string sectionName, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        //{
        //    var Query = GetReportQuery(no, buyerCode, sectionName, dateFrom, dateTo, offset);

        //    Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
        //    if (OrderDictionary.Count.Equals(0))
        //    {
        //        Query = Query.OrderByDescending(b => b.LastModifiedUtc);
        //    }

        //    Pageable<GarmentBookingOrderMonitoringViewModel> pageable = new Pageable<GarmentBookingOrderMonitoringViewModel>(Query, page - 1, size);
        //    List<GarmentBookingOrderMonitoringViewModel> Data = pageable.Data.ToList<GarmentBookingOrderMonitoringViewModel>();
        //    int TotalData = pageable.TotalCount;

        //    return Tuple.Create(Data, TotalData);
        //}

        //public MemoryStream GenerateExcel(string no, string buyerCode, string orderTypeCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        //{
        //    var Query = GetReportQuery(no, buyerCode, orderTypeCode, comodityCode, dateFrom, dateTo, offset);
        //    Query = Query.OrderByDescending(b => b.LastModifiedUtc);
        //    DataTable result = new DataTable();
        //    //No	Unit	Budget	Kategori	Tanggal PR	Nomor PR	Kode Barang	Nama Barang	Jumlah	Satuan	Tanggal Diminta Datang	Status	Tanggal Diminta Datang Eksternal


        //    result.Columns.Add(new DataColumn() { ColumnName = "Kode Booking", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Booking", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengeriman (booking)", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Confirm", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengiriman (confirm)", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Confirm", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Status Confirm", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Status Booking Order", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Sisa Order (Belum Confirm)", DataType = typeof(String) });
        //    result.Columns.Add(new DataColumn() { ColumnName = "Seilisih Hari (dari Tanggal Pengiriman)", DataType = typeof(double) });
        //    if (Query.ToArray().Count() == 0)
        //        result.Rows.Add("", "", "", 0, "", "", 0, "", "", "", "", "", 0, ""); // to allow column name to be generated properly for empty data as template
        //    else
        //    {
        //        int index = 0;
        //        foreach (var item in Query)
        //        {
        //            index++;
        //            DateTimeOffset date = item.deliverySchedule ?? new DateTime(1970, 1, 1);
        //            string deliverySchedule = date == new DateTime(1970, 1, 1) ? "-" : date.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
        //            result.Rows.Add(index, item.salesContractNo, item.CreatedUtc.ToString("dd MMM yyyy", new CultureInfo("id-ID")), item.buyerName, item.buyerType,
        //                item.dispositionNo, item.orderType, item.comodityName, item.materialName, item.materialConstructionName, item.yarnMaterialName, item.materialWidth,
        //                item.qualityName, item.orderQuantity, item.productionOrderQuantity, item.uomUnit, item.shippingQuantityTolerance, item.termOfPaymentName, item.paymentTo, deliverySchedule,
        //                item.agentName, item.comission, item.color, item.price, item.accountCurrencyCode, item.useIncomeTax, item.status);
        //        }
        //    }

        //    return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        //}
    }
}
