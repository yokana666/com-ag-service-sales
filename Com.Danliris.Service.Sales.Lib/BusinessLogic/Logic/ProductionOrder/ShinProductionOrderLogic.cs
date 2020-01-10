using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder
{
    public class ShinProductionOrderLogic : BaseLogic<ProductionOrderModel>
    {
        private ProductionOrder_DetailLogic productionOrder_DetailLogic;
        private ProductionOrder_LampStandardLogic productionOrder_LampStandardLogic;
        private ProductionOrder_RunWidthLogic productionOrder_RunWidthLogic;

        public ShinProductionOrderLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
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
              "OrderNo", "SalesContractNo", "BuyerType", "BuyerName", "ProcessTypeName"
            };

            Query = QueryHelper<ProductionOrderModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ProductionOrderModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {

                "Id", "Code", "Buyer", "ProcessType", "LastModifiedUtc", "FinishingPrintingSalesContract", "OrderNo", "Details", "OrderType", "HandlingStandard", "Material", "YarnMaterial", "DeliveryDate", "SalesContractNo", "MaterialConstruction", "FinishWidth", "DesignCode", "DesignNumber", "OrderQuantity", "Uom",
                "DistributedQuantity", "IsCompleted", "IsClosed", "IsCalculated", "Account"

            };

            

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ProductionOrderModel>.Order(Query, OrderDictionary);

            Pageable<ProductionOrderModel> pageable = new Pageable<ProductionOrderModel>(Query, page - 1, size);
            List<ProductionOrderModel> data = pageable.Data.ToList<ProductionOrderModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductionOrderModel>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
