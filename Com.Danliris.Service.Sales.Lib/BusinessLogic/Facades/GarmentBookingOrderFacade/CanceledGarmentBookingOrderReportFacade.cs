using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade
{
    public class CanceledGarmentBookingOrderReportFacade : ICanceledGarmentBookingOrderReportFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentBookingOrder> DbSet;
        private IdentityService IdentityService;
        private GarmentBookingOrderLogic GarmentBookingOrderLogic;
        public CanceledGarmentBookingOrderReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<GarmentBookingOrder>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.GarmentBookingOrderLogic = serviceProvider.GetService<GarmentBookingOrderLogic>();
        }

        public IQueryable<CanceledGarmentBookingOrderReportViewModel> GetReportQuery(string no, string buyerCode, string statusCancel, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            List<CanceledGarmentBookingOrderReportViewModel> listGarmentBookingReport = new List<CanceledGarmentBookingOrderReportViewModel>();

            var Query = (from a in DbContext.GarmentBookingOrders
                         join b in DbContext.GarmentBookingOrderItems on a.Id equals b.BookingOrderId
                         //Conditions
                         where a.IsDeleted == false
                             && a.BookingOrderNo == (string.IsNullOrWhiteSpace(no) ? a.BookingOrderNo : no)
                             && a.BuyerCode == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
                             && a.BookingOrderDate.AddHours(offset).Date >= DateFrom.Date
                             && a.BookingOrderDate.AddHours(offset).Date <= DateTo.Date
                         select new CanceledGarmentBookingOrderReportViewModel
                         {
                             CreatedUtc = a.CreatedUtc,
                             BookingOrderDate = a.BookingOrderDate,
                             BookingOrderNo = a.BookingOrderNo,
                             BuyerName = a.BuyerName,
                             OrderQuantity = a.OrderQuantity,
                             CanceledQuantity = a.CanceledQuantity,
                             ExpiredBookingQuantity = a.ExpiredBookingQuantity,
                             DeliveryDate = a.DeliveryDate,
                             ComodityName = b.ComodityName,
                             ConfirmQuantity = b.ConfirmQuantity,
                             ConfirmDate = b.ConfirmDate,
                             DeliveryDateItem = b.DeliveryDate,
                             Remark = b.Remark,
                             CanceledDate = a.CanceledDate,
                             CanceledDateItem = b.CanceledDate,
                             ExpiredBookingDate = a.ExpiredBookingDate,
                             CancelStatus = "",
                             IsCanceled = b.IsCanceled,
                             LastModifiedUtc = a.LastModifiedUtc
                         });

            foreach(var query in Query)
            {
                if (query.IsCanceled == true)
                {
                    CanceledGarmentBookingOrderReportViewModel view = new CanceledGarmentBookingOrderReportViewModel
                    {
                        CreatedUtc = query.CreatedUtc,
                        BookingOrderDate = query.BookingOrderDate,
                        BookingOrderNo = query.BookingOrderNo,
                        BuyerName = query.BuyerName,
                        OrderQuantity = query.OrderQuantity,
                        DeliveryDate = query.DeliveryDate,
                        ComodityName = query.ComodityName,
                        ConfirmQuantity = query.ConfirmQuantity,
                        ConfirmDate = query.ConfirmDate,
                        DeliveryDateItem = query.DeliveryDateItem,
                        Remark = query.Remark,
                        CanceledDate = query.CanceledDateItem,
                        CanceledQuantity = query.ConfirmQuantity,
                        ExpiredBookingQuantity = query.ExpiredBookingQuantity,
                        CancelStatus = "Cancel Confirm"
                    };
                    listGarmentBookingReport.Add(view);
                }
                if (query.CanceledQuantity > 0)
                {
                    CanceledGarmentBookingOrderReportViewModel view = new CanceledGarmentBookingOrderReportViewModel
                    {
                        CreatedUtc = query.CreatedUtc,
                        BookingOrderDate = query.BookingOrderDate,
                        BookingOrderNo = query.BookingOrderNo,
                        BuyerName = query.BuyerName,
                        OrderQuantity = query.OrderQuantity,
                        DeliveryDate = query.DeliveryDate,
                        ComodityName = null,
                        ConfirmQuantity = null,
                        ConfirmDate = null,
                        DeliveryDateItem = null,
                        Remark = null,
                        CanceledDate = query.CanceledDate,
                        CanceledQuantity = query.CanceledQuantity,
                        ExpiredBookingQuantity = query.ExpiredBookingQuantity,
                        CancelStatus = "Cancel Sisa"
                    };
                    listGarmentBookingReport.Add(view);
                }
                if (query.ExpiredBookingQuantity > 0)
                {
                    CanceledGarmentBookingOrderReportViewModel view = new CanceledGarmentBookingOrderReportViewModel
                    {
                        CreatedUtc = query.CreatedUtc,
                        BookingOrderDate = query.BookingOrderDate,
                        BookingOrderNo = query.BookingOrderNo,
                        BuyerName = query.BuyerName,
                        OrderQuantity = query.OrderQuantity,
                        DeliveryDate = query.DeliveryDate,
                        ComodityName = null,
                        ConfirmQuantity = null,
                        ConfirmDate = null,
                        DeliveryDateItem = null,
                        Remark = null,
                        CanceledDate = query.ExpiredBookingDate,
                        CanceledQuantity = query.ExpiredBookingQuantity,
                        ExpiredBookingQuantity = query.ExpiredBookingQuantity,
                        CancelStatus = "Expired"
                    };
                    listGarmentBookingReport.Add(view);
                }
            }
            if (statusCancel != null)
            {
                return listGarmentBookingReport.Where(m => m.CancelStatus == statusCancel).AsQueryable();
            }
            else
            {
                return listGarmentBookingReport.AsQueryable();
            }
        }

        public Tuple<List<CanceledGarmentBookingOrderReportViewModel>, int> Read(string no, string buyerCode, string statusCancel, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(no, buyerCode, statusCancel, dateFrom, dateTo, offset);
            var statusCheck = "";
            var bookingOrderNoTemp = "";
            List<CanceledGarmentBookingOrderReportViewModel> Data = new List<CanceledGarmentBookingOrderReportViewModel>();
            foreach (var item in Query.OrderByDescending(b => b.LastModifiedUtc).ThenBy(b => b.BookingOrderNo).ThenBy(b => b.CancelStatus))
            {
                CanceledGarmentBookingOrderReportViewModel _new = new CanceledGarmentBookingOrderReportViewModel
                {
                    CreatedUtc = item.CreatedUtc,
                    BookingOrderDate = item.BookingOrderDate,
                    BookingOrderNo = item.BookingOrderNo,
                    BuyerName = item.BuyerName,
                    OrderQuantity = item.OrderQuantity,
                    DeliveryDate = item.DeliveryDate,
                    ComodityName = item.ComodityName,
                    ConfirmDate = item.ConfirmDate,
                    ConfirmQuantity = item.ConfirmQuantity,
                    DeliveryDateItem = item.DeliveryDateItem,
                    Remark = item.Remark,
                    CanceledDate = item.CanceledDate,
                    CanceledQuantity = item.CanceledQuantity,
                    ExpiredBookingQuantity = item.ExpiredBookingQuantity,
                    CancelStatus = item.CancelStatus
                };


                if (bookingOrderNoTemp == item.BookingOrderNo && statusCheck == item.CancelStatus && statusCheck != "Cancel Confirm")
                    _new = null;
                else
                    Data.Add(_new);

                if (bookingOrderNoTemp == "" || bookingOrderNoTemp != item.BookingOrderNo)
                    bookingOrderNoTemp = item.BookingOrderNo;

                if(statusCheck == "" || statusCheck != item.CancelStatus)
                    statusCheck = item.CancelStatus;
            }
            return Tuple.Create(Data, Data.Count);
        }

        public MemoryStream GenerateExcel(string no, string buyerCode, string statusCancel, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(no, buyerCode, statusCancel, dateFrom, dateTo, offset);
            var statusCheck = "";
            var bookingOrderNoTemp = "";
            List<CanceledGarmentBookingOrderReportViewModel> Data = new List<CanceledGarmentBookingOrderReportViewModel>();
            Query = Query.OrderByDescending(b => b.LastModifiedUtc).ThenBy(b => b.BookingOrderNo).ThenBy(b => b.CancelStatus);

            foreach (var item in Query)
            {
                CanceledGarmentBookingOrderReportViewModel _new = new CanceledGarmentBookingOrderReportViewModel
                {
                    CreatedUtc = item.CreatedUtc,
                    BookingOrderDate = item.BookingOrderDate,
                    BookingOrderNo = item.BookingOrderNo,
                    BuyerName = item.BuyerName,
                    OrderQuantity = item.OrderQuantity,
                    DeliveryDate = item.DeliveryDate,
                    ComodityName = item.ComodityName,
                    ConfirmDate = item.ConfirmDate,
                    ConfirmQuantity = item.ConfirmQuantity,
                    DeliveryDateItem = item.DeliveryDateItem,
                    Remark = item.Remark,
                    CanceledDate = item.CanceledDate,
                    CanceledQuantity = item.CanceledQuantity,
                    ExpiredBookingQuantity = item.ExpiredBookingQuantity,
                    CancelStatus = item.CancelStatus
                };


                if (bookingOrderNoTemp == item.BookingOrderNo && statusCheck == item.CancelStatus && statusCheck != "Cancel Confirm")
                    _new = null;
                else
                    Data.Add(_new);

                if (bookingOrderNoTemp == "" || bookingOrderNoTemp != item.BookingOrderNo)
                    bookingOrderNoTemp = item.BookingOrderNo;

                if (statusCheck == "" || statusCheck != item.CancelStatus)
                    statusCheck = item.CancelStatus;
            }

            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Booking", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Booking", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Booking Order Awal", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Booking Order Akhir", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Pengiriman (Booking)", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jml Confirm", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Confirm", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Pengiriman (Confirm)", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Cancel", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah yg Dicancel", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status Cancel", DataType = typeof(String) });
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Data)
                {
                    index++;
                    DateTimeOffset bookingOrderDate = item.BookingOrderDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset deliveryDate = item.DeliveryDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset confirmDate = item.ConfirmDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset deliveryDateItem = item.DeliveryDateItem ?? new DateTime(1970, 1, 1);
                    DateTimeOffset canceledDate = item.CanceledDate ?? new DateTime(1970, 1, 1);

                    string BookingOrderDate = bookingOrderDate == new DateTime(1970, 1, 1) ? "-" : bookingOrderDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDate = deliveryDate == new DateTime(1970, 1, 1) ? "-" : deliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ConfirmDate = confirmDate == new DateTime(1970, 1, 1) ? "-" : confirmDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDateItem = deliveryDateItem == new DateTime(1970, 1, 1) ? "-" : deliveryDateItem.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string CanceledDate = canceledDate == new DateTime(1970, 1, 1) ? "-" : canceledDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    var total = item.OrderQuantity + item.CanceledQuantity + item.ExpiredBookingQuantity;
                    result.Rows.Add(index, item.BookingOrderNo, item.BookingOrderDate, item.BuyerName, total, 
                        item.OrderQuantity, item.DeliveryDate, item.ComodityName, item.ConfirmQuantity, item.ConfirmDate, item.DeliveryDateItem, item.Remark, item.CanceledDate, item.CanceledQuantity, item.CancelStatus);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }
    }
}
