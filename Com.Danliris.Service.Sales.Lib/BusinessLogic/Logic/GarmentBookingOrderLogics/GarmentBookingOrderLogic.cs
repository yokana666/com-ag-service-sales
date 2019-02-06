using Com.Danliris.Service.Sales.Lib.Models.BookingOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics
{
    public class GarmentBookingOrderLogic : BaseLogic<GarmentBookingOrder>
    {
        private readonly SalesDbContext DbContext;
        public GarmentBookingOrderLogic(IIdentityService IdentityService, IServiceProvider serviceProvider, SalesDbContext dbContext) : base(IdentityService, serviceProvider, dbContext)
        {
        }

        public override void Create(GarmentBookingOrder model)
        {
            GenerateBookingOrderNo(model);

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        private void GenerateBookingOrderNo(GarmentBookingOrder model)
        {
            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");

            string no = $"{model.SectionCode}-{model.BuyerCode}-{Year}-";
            int Padding = 5;

            var lastData = DbSet.IgnoreQueryFilters().Where(w => w.BookingOrderNo.StartsWith(no) && !w.IsDeleted).OrderByDescending(o => o.BookingOrderNo).FirstOrDefault();
            // DbContext
            if (lastData == null)
            {
                model.BookingOrderNo = no + "1".PadLeft(Padding, '0');
            }
            else
            {
                int lastNoNumber = Int32.Parse(lastData.BookingOrderNo.Replace(no, "")) + 1;
                model.BookingOrderNo = no + lastNoNumber.ToString().PadLeft(Padding, '0');
            }

        }

        public override ReadResponse<GarmentBookingOrder> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentBookingOrder> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "BookingOrderNo","BuyerName"
            };

            Query = QueryHelper<GarmentBookingOrder>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentBookingOrder>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                  "Id", "BookingOrderNo", "BookingOrderDate", "SectionName", "BuyerName", "OrderQuantity", "LastModifiedUtc","Remark",
                    "IsBlockingPlan", "IsCanceled", "CanceledDate", "DeliveryDate", "CanceledQuantity", "ExpiredBookingDate", "ExpiredBookingQuantity",
                      "ConfirmedQuantity", "HadConfirmed"
            };

            Query = Query
                 .Select(bo => new GarmentBookingOrder
                 {
                     Id = bo.Id,
                     BookingOrderNo = bo.BookingOrderNo,
                     BookingOrderDate = bo.BookingOrderDate,
                     BuyerCode = bo.BuyerCode,
                     BuyerId = bo.BuyerId,
                     BuyerName = bo.BuyerName,
                     SectionId = bo.SectionId,
                     SectionCode = bo.SectionCode,
                     SectionName = bo.SectionName,
                     DeliveryDate = bo.DeliveryDate,
                     OrderQuantity = bo.OrderQuantity,
                     Remark = bo.Remark,
                     IsBlockingPlan = bo.IsBlockingPlan,
                     IsCanceled = bo.IsCanceled,
                     CanceledDate = bo.CanceledDate,
                     CanceledQuantity = bo.CanceledQuantity,
                     ExpiredBookingDate = bo.ExpiredBookingDate,
                     ExpiredBookingQuantity = bo.ExpiredBookingQuantity,
                     ConfirmedQuantity = bo.ConfirmedQuantity,
                     HadConfirmed = bo.HadConfirmed,
                     LastModifiedUtc = bo.LastModifiedUtc,
                 });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentBookingOrder>.Order(Query, OrderDictionary);

            Pageable<GarmentBookingOrder> pageable = new Pageable<GarmentBookingOrder>(Query, page - 1, size);
            List<GarmentBookingOrder> data = pageable.Data.ToList<GarmentBookingOrder>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentBookingOrder>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
