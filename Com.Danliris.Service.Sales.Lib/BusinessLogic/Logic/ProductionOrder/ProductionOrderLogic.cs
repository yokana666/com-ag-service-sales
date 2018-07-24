using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder
{
    public class ProductionOrderLogic : BaseLogic<ProductionOrderModel>
    {
        private ProductionOrder_DetailLogic productionOrder_DetailLogic;
        private ProductionOrder_LampStandardLogic productionOrder_LampStandardLogic;
        private ProductionOrder_RunWidthLogic productionOrder_RunWidthLogic;

        public ProductionOrderLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.productionOrder_DetailLogic = serviceProvider.GetService<ProductionOrder_DetailLogic>();
            this.productionOrder_LampStandardLogic = serviceProvider.GetService<ProductionOrder_LampStandardLogic>();
            this.productionOrder_RunWidthLogic = serviceProvider.GetService<ProductionOrder_RunWidthLogic>();

        }
        public override ReadResponse<ProductionOrderModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ProductionOrderModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<ProductionOrderModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ProductionOrderModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Buyer", "LastModifiedUtc","SalesContractNo","OrderNo"
            };

            Query = Query
                .Select(field => new ProductionOrderModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    SalesContractNo = field.SalesContractNo,
                    BuyerType = field.BuyerType,
                    BuyerName = field.BuyerName,
                    //DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ProductionOrderModel>.Order(Query, OrderDictionary);

            Pageable<ProductionOrderModel> pageable = new Pageable<ProductionOrderModel>(Query, page - 1, size);
            List<ProductionOrderModel> data = pageable.Data.ToList<ProductionOrderModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductionOrderModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override async void UpdateAsync(int id, ProductionOrderModel model)
        {

            if (model.Details != null)
            {
                HashSet<long> detailIds = productionOrder_DetailLogic.GetIds(id);
                foreach (var itemId in detailIds)
                {
                    ProductionOrder_DetailModel data = model.Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await productionOrder_DetailLogic.DeleteAsync(Convert.ToInt32(itemId));
                    else
                    {
                        productionOrder_DetailLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                    }

                    foreach (ProductionOrder_DetailModel item in model.Details)
                    {
                        if (item.Id == 0)
                            productionOrder_DetailLogic.Create(item);
                    }
                }

                HashSet<long> LampStandardIds = productionOrder_LampStandardLogic.GetIds(id);
                foreach (var itemId in detailIds)
                {
                    ProductionOrder_LampStandardModel data = model.LampStandards.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await productionOrder_LampStandardLogic.DeleteAsync(Convert.ToInt32(itemId));
                    else
                    {
                        productionOrder_LampStandardLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                    }

                    foreach (ProductionOrder_LampStandardModel item in model.LampStandards)
                    {
                        if (item.Id == 0)
                            productionOrder_LampStandardLogic.Create(item);
                    }
                }

                if (model.RunWidths.Count > 0)
                {
                    HashSet<long> RunWidthIds = productionOrder_RunWidthLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        ProductionOrder_RunWidthModel data = model.RunWidths.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await productionOrder_RunWidthLogic.DeleteAsync(Convert.ToInt32(itemId));
                        else
                        {
                            productionOrder_RunWidthLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                        }

                        foreach (ProductionOrder_RunWidthModel item in model.RunWidths)
                        {
                            if (item.Id == 0)
                                productionOrder_RunWidthLogic.Create(item);
                        }
                    }
                }

            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }


        public override void Create(ProductionOrderModel model)
        {
            if (model.Details.Count > 0)
            {
                foreach (var detail in model.Details)
                {
                    productionOrder_DetailLogic.Create(detail);
                }
            }

            if (model.LampStandards.Count>0) {
                foreach (var lampStandards in model.LampStandards)
                {
                    lampStandards.Id = 0;
                    productionOrder_LampStandardLogic.Create(lampStandards);
                }
            }

            if (model.RunWidths.Count > 0)
            {
                foreach (var runWidths in model.RunWidths)
                {
                    productionOrder_RunWidthLogic.Create(runWidths);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(int id)
        {

            ProductionOrderModel model = await ReadByIdAsync(id);

            foreach (var detail in model.Details)
            {
                await productionOrder_DetailLogic.DeleteAsync(Convert.ToInt16(detail.Id));
            }

            foreach (var lampStandards in model.LampStandards)
            {
                await productionOrder_LampStandardLogic.DeleteAsync(Convert.ToInt16(lampStandards.Id));
            }


            if (model.RunWidths.Count > 0)
            {
                foreach (var runWidths in model.RunWidths)
                {
                    await productionOrder_RunWidthLogic.DeleteAsync(Convert.ToInt16(runWidths.Id));
                }
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }
    }
}
