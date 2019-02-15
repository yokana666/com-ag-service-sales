using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
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

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics
{
    public class WeeklyPlanLogic : BaseLogic<WeeklyPlan>
    {
        public WeeklyPlanLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
        }

        public override ReadResponse<WeeklyPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<WeeklyPlan> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "UnitCode", "UnitName"
            };

            Query = QueryHelper<WeeklyPlan>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<WeeklyPlan>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select ?? new List<string>()
            {
                "Id", "Year", "Unit"
            };

            Query = Query
                .Select(field => new WeeklyPlan
                {
                    Id = field.Id,
                    Year = field.Year,
                    UnitId = field.UnitId,
                    UnitCode = field.UnitCode,
                    UnitName = field.UnitName,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<WeeklyPlan>.Order(Query, OrderDictionary);

            Pageable<WeeklyPlan> pageable = new Pageable<WeeklyPlan>(Query, page - 1, size);
            List<WeeklyPlan> data = pageable.Data.ToList<WeeklyPlan>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<WeeklyPlan>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(WeeklyPlan model)
        {
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
            }
            base.Create(model);
        }

        public override async Task<WeeklyPlan> ReadByIdAsync(int id)
        {
            var model = await DbSet.AsNoTracking().Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            model.Items = model.Items.OrderBy(i => i.WeekNumber).ToList();
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

        public override void UpdateAsync(int id, WeeklyPlan newModel)
        {
            var model = DbSet.Include(d => d.Items).FirstOrDefault(d => d.Id == id);
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.Items)
            {
                var newItem = newModel.Items.Single(i => i.Id == item.Id);
                item.Efficiency = newItem.Efficiency;
                item.Operator = newItem.Operator;
                item.WorkingHours = newItem.WorkingHours;
                item.AHTotal = newItem.AHTotal;
                item.EHTotal = newItem.EHTotal;
                item.RemainingEH = newItem.RemainingEH;
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");
            }
        }
    }
}
