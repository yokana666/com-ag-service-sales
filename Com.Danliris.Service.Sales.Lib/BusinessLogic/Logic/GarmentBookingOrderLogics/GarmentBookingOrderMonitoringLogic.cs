using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics
{
    public class GarmentBookingOrderMonitoringLogic //: BaseMonitoringLogic<GarmentBookingOrderMonitoringViewModel>
    {
        //private IIdentityService identityService;
        //private SalesDbContext dbContext;
        //private DbSet<GarmentBookingOrder> dbSet;

        //public GarmentBookingOrderMonitoringLogic(IIdentityService identityService, SalesDbContext dbContext)
        //{
        //    this.identityService = identityService;
        //    this.dbContext = dbContext;
        //    dbSet = dbContext.Set<GarmentBookingOrder>();
        //}

        //public override IQueryable<GarmentBookingOrderMonitoringViewModel> GetQuery(string filter)
        //{
        //    Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

        //    IQueryable<GarmentBookingOrder> Query = dbSet.Include(i => i.Items);

        //    if (FilterDictionary.TryGetValue("BookingOrderNo", out string BookingOrderNo))
        //    {
        //        Query = Query.Where(d => d.BookingOrderNo == BookingOrderNo);
        //    }

        //    //DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
        //    //DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
        //    Query = Query.OrderBy(o => o.BookingOrderNo);

        //    return Query.Select(d => new GarmentBookingOrderMonitoringViewModel
        //    {
        //        BookingOrderNo = d.BookingOrderNo,
        //        BookingOrderDate = d.BookingOrderDate,
        //        BuyerName = d.BuyerName,
        //        ComodityName = d.Items.Single(a => d.Id == a.BookingOrderId).ComodityName,
        //        SectionName = d.SectionName,
        //        ConfirmDate = d.Items.Single(a => d.Id == a.BookingOrderId).ConfirmDate,
        //        ConfirmQuantity = d.Items.Single(a => d.Id == a.BookingOrderId).ConfirmQuantity,
        //        DeliveryDate = d.DeliveryDate,
        //        DeliveryDateItems = d.Items.Single(a => d.Id == a.BookingOrderId).DeliveryDate,
        //        OrderQuantity = d.OrderQuantity,
        //        Remark = d.Remark,
        //    });
        //}
    }
}
