using Com.Danliris.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
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
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics
{
    public class GarmentSewingBlockingPlanLogic : BaseLogic<GarmentSewingBlockingPlan>
    {
        public GarmentSewingBlockingPlanLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
        }

        public override ReadResponse<GarmentSewingBlockingPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentSewingBlockingPlan> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "BookingOrderNo", "BuyerName"
            };

            Query = QueryHelper<GarmentSewingBlockingPlan>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentSewingBlockingPlan>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select ?? new List<string>()
            {
                "BookingOrderNo", "Buyer", "DeliveryDate", "BookingOrderDate","Remark"
            };

            Query = Query
                .Select(field => new GarmentSewingBlockingPlan
                {
                    Id = field.Id,
                    BookingOrderNo=field.BookingOrderNo,
                    BookingOrderDate=field.BookingOrderDate,
                    BuyerName=field.BuyerName,
                    OrderQuantity=field.OrderQuantity,
                    DeliveryDate=field.DeliveryDate,
                    Remark=field.Remark,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentSewingBlockingPlan>.Order(Query, OrderDictionary);

            Pageable<GarmentSewingBlockingPlan> pageable = new Pageable<GarmentSewingBlockingPlan>(Query, page - 1, size);
            List<GarmentSewingBlockingPlan> data = pageable.Data.ToList<GarmentSewingBlockingPlan>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentSewingBlockingPlan>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(GarmentSewingBlockingPlan model)
        {
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
            }
            base.Create(model);
        }

        public override async Task<GarmentSewingBlockingPlan> ReadByIdAsync(int id)
        {
            var model = await DbSet.AsNoTracking().Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            model.Items = model.Items.ToList();
            return model;
        }

        public override async Task DeleteAsync(int id)
        {
            var model = await DbSet.Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);
            }
        }

        public override void UpdateAsync(int id, GarmentSewingBlockingPlan newModel)
        {
            var model = DbSet.Include(d => d.Items).FirstOrDefault(d => d.Id == id);
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.Items)
            {
                var newItem = newModel.Items.Single(i => i.Id == item.Id);
                item.Efficiency = newItem.Efficiency;

                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");
            }
        }
    }
}
