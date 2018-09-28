using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Com.Danliris.Service.Sales.Lib.Utilities;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics
{
    public class ROGarmentLogic : BaseLogic<RO_Garment>
    {
        private ROGarmentSizeBreakdownLogic roGarmentSizeBreakdownLogic;
        private ROGarmentSizeBreakdownDetailLogic roGarmentSizeBreakdownDetailLogic;

        private readonly SalesDbContext DbContext;
        public ROGarmentLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.roGarmentSizeBreakdownLogic = serviceProvider.GetService<ROGarmentSizeBreakdownLogic>();
            this.roGarmentSizeBreakdownDetailLogic = serviceProvider.GetService<ROGarmentSizeBreakdownDetailLogic>();
            this.DbContext = dbContext;
        }

        public override ReadResponse<RO_Garment> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<RO_Garment> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = QueryHelper<RO_Garment>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<RO_Garment>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                  "Id", "Code", "CostCalculationGarment", "Total"
            };

            Query = Query
                 .Select(ccg => new RO_Garment
                 {
                     Id = ccg.Id,
                     Code = ccg.Code,
                     CostCalculationGarment=ccg.CostCalculationGarment,
                     CostCalculationGarmentId=ccg.CostCalculationGarmentId,
                     LastModifiedUtc = ccg.LastModifiedUtc
                 });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<RO_Garment>.Order(Query, OrderDictionary);

            Pageable<RO_Garment> pageable = new Pageable<RO_Garment>(Query, page - 1, size);
            List<RO_Garment> data = pageable.Data.ToList<RO_Garment>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<RO_Garment>(data, totalData, OrderDictionary, SelectedFields);
        }


        public override void Create(RO_Garment model)
        {
            do
            {
                model.Code = Code.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            foreach (var size in model.RO_Garment_SizeBreakdowns)
            {
                roGarmentSizeBreakdownLogic.Create(size);
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async void UpdateAsync(int id, RO_Garment model)
        {
            if (model.RO_Garment_SizeBreakdowns != null)
            {
                HashSet<long> detailIds = roGarmentSizeBreakdownLogic.GetIds(id);
                foreach (var itemId in detailIds)
                {
                    RO_Garment_SizeBreakdown data = model.RO_Garment_SizeBreakdowns.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                    {
                        await roGarmentSizeBreakdownLogic.DeleteAsync(Convert.ToInt32(itemId));

                    }
                    else
                    {
                        roGarmentSizeBreakdownLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                    }

                    foreach (RO_Garment_SizeBreakdown item in model.RO_Garment_SizeBreakdowns)
                    {
                        if (item.Id == 0)
                            roGarmentSizeBreakdownLogic.Create(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

    }
}
