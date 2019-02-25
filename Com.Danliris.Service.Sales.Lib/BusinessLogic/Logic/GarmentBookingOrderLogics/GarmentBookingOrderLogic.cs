using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
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
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics
{
    public class GarmentBookingOrderLogic : BaseLogic<GarmentBookingOrder>
    {
        private readonly SalesDbContext DbContext;
        private GarmentBookingOrderItemLogic GarmentBookingOrderItemsLogic;
        public GarmentBookingOrderLogic(GarmentBookingOrderItemLogic GarmentBookingOrderItemsLogic, IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
            this.GarmentBookingOrderItemsLogic = GarmentBookingOrderItemsLogic;
            this.DbContext = dbContext;
        }

        public override void Create(GarmentBookingOrder model)
        {
            GenerateBookingOrderNo(model);
            if (model.Items.Count > 0)
            {
                model.HadConfirmed = true;
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }
        public override async Task<GarmentBookingOrder> ReadByIdAsync(int id)
        {
            var garmentBookingOrder = await DbSet.Include(p => p.Items).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            garmentBookingOrder.Items = garmentBookingOrder.Items.Where(s => s.IsCanceled == false).OrderBy(s => s.Id).ToArray();
            return garmentBookingOrder;
        }

        public override async void UpdateAsync(int id, GarmentBookingOrder model)
        {
            double updatedItems = 0;
            model.HadConfirmed = true;
            if (model.Items != null)
            {
                HashSet<long> itemIds = GarmentBookingOrderItemsLogic.GetBookingOrderIds(id);
                foreach (var itemId in itemIds)
                {
                    GarmentBookingOrderItem data = model.Items.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                    {
                        GarmentBookingOrderItem dataItem = DbContext.GarmentBookingOrderItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                        model.ConfirmedQuantity -= dataItem.ConfirmQuantity;
                        if (dataItem.IsCanceled == false && dataItem.IsDeleted == true && model.ConfirmedQuantity == 0)
                        {
                            model.HadConfirmed = false;
                        }
                    }
                    else
                    {
                        if (data.IsCanceled)
                        {
                            var itemz = DbContext.GarmentBookingOrderItems.Where(i => i.BookingOrderId == id && i.IsCanceled == false).Sum(s => s.ConfirmQuantity);
                            GarmentBookingOrderItemsLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                            data.CanceledDate = DateTimeOffset.Now;
                            model.ConfirmedQuantity = itemz - data.ConfirmQuantity;
                        }
                        else
                        {
                            GarmentBookingOrderItemsLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                            updatedItems += data.ConfirmQuantity;
                            model.ConfirmedQuantity = updatedItems;
                        }
                    }
                }

                foreach (GarmentBookingOrderItem item in model.Items)
                {
                    if (item.Id == 0)
                    {
                        GarmentBookingOrderItemsLogic.Create(item);
                        model.ConfirmedQuantity += item.ConfirmQuantity;
                    }
                }

            }
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
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
                      "ConfirmedQuantity", "HadConfirmed","Items","BuyerCode","BuyerId"
            };

            Query = Query.Where(d => d.OrderQuantity > 0)
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
                     Items = bo.Items.ToList()
                 });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentBookingOrder>.Order(Query, OrderDictionary);

            Pageable<GarmentBookingOrder> pageable = new Pageable<GarmentBookingOrder>(Query, page - 1, size);
            List<GarmentBookingOrder> data = pageable.Data.ToList<GarmentBookingOrder>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentBookingOrder>(data, totalData, OrderDictionary, SelectedFields);
        }

        public async Task BOCancel(int id, GarmentBookingOrder model)
        {
            double cancelsQuantity = 0;

            cancelsQuantity = model.OrderQuantity - model.ConfirmedQuantity;

            model.CanceledQuantity = cancelsQuantity;
            model.OrderQuantity -= cancelsQuantity;
            model.CanceledDate = DateTimeOffset.Now;
            foreach (var item in model.Items)
            {
                GarmentBookingOrderItemsLogic.UpdateAsync((int)item.Id, item);
            }

            if (model.ConfirmedQuantity == 0)
            {
                model.IsCanceled = true;
            }
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");

            DbSet.Update(model);
        }

        public async Task BODelete(int id, GarmentBookingOrder model )
        {
            double cancelsQuantity = 0;

            cancelsQuantity = model.OrderQuantity - model.ConfirmedQuantity;

            model.ExpiredBookingQuantity = cancelsQuantity;
            model.OrderQuantity -= cancelsQuantity;
            model.ExpiredBookingDate = DateTimeOffset.Now;

            foreach (var item in model.Items)
            {
                GarmentBookingOrderItemsLogic.UpdateAsync((int)item.Id, item);
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");

            DbSet.Update(model);
        }
    }
}
