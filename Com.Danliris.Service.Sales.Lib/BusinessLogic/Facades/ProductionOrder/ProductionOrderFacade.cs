using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder
{
    public class ProductionOrderFacade : IProductionOrder
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<ProductionOrderModel> DbSet;
        private readonly IIdentityService identityService;
        private readonly ProductionOrderLogic productionOrderLogic;
        private readonly FinishingPrintingSalesContractLogic finishingPrintingSalesContractLogic;

        public ProductionOrderFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<ProductionOrderModel>();
            this.identityService = serviceProvider.GetService<IIdentityService>();
            this.productionOrderLogic = serviceProvider.GetService<ProductionOrderLogic>();
            this.finishingPrintingSalesContractLogic = serviceProvider.GetService<FinishingPrintingSalesContractLogic>();
        }

        public async Task<int> CreateAsync(ProductionOrderModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    List<object> productionOrderModelTemp = new List<object>();

                    List<ProductionOrder_DetailModel> DetailsTemp = new List<ProductionOrder_DetailModel>();
                    foreach (ProductionOrder_DetailModel dataTemp in model.Details)
                    {
                        DetailsTemp.Add(dataTemp);
                    }
                    ProductionOrderModel productionOrderModel = new ProductionOrderModel();
                    productionOrderModel = model;

                    int index = 0;

                    for (int i = 0; i < DetailsTemp.Count; i++)
                    {

                        List<ProductionOrder_RunWidthModel> runWidthTemp = new List<ProductionOrder_RunWidthModel>();
                        if (model.RunWidths.Count > 0)
                        {
                            foreach (ProductionOrder_RunWidthModel runWidth in model.RunWidths)
                            {
                                runWidthTemp.Add(runWidth);
                            }
                        }

                        productionOrderModel.RunWidths = runWidthTemp;


                        List<ProductionOrder_LampStandardModel> LampStandardsTemp = new List<ProductionOrder_LampStandardModel>();

                        foreach (ProductionOrder_LampStandardModel LampStandardModel in model.LampStandards)
                        {
                            LampStandardsTemp.Add(LampStandardModel);
                        }

                        productionOrderModel.LampStandards = LampStandardsTemp;

                        do
                        {
                            productionOrderModel.Code = CodeGenerator.Generate();
                        }
                        while (DbSet.Any(d => d.Code.Equals(productionOrderModel.Code)));

                        index += i;
                        ProductionOrderNumberGenerator(productionOrderModel, index);

                        var temp = productionOrderModel.Clone();

                        productionOrderLogic.Create(temp);
                    }

                    FinishingPrintingSalesContractModel dataFPSalesContract = await finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(productionOrderModel.SalesContractId));
                    if (dataFPSalesContract != null)
                    {
                        dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity - model.OrderQuantity;
                        this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    ProductionOrderModel model = await productionOrderLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        ProductionOrderModel productionOrderModel = new ProductionOrderModel();

                        productionOrderModel = model;
                        await productionOrderLogic.DeleteAsync(id);
                        FinishingPrintingSalesContractModel dataFPSalesContract = await this.finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(productionOrderModel.SalesContractId));
                        if (dataFPSalesContract != null)
                        {
                            dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity + model.OrderQuantity;
                            this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                        }
                    }
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<ProductionOrderModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return productionOrderLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<ProductionOrderModel> ReadByIdAsync(int id)
        {
            return await productionOrderLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, ProductionOrderModel model)
        {

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    productionOrderLogic.UpdateAsync(id, model);
                    FinishingPrintingSalesContractModel dataFPSalesContract = await this.finishingPrintingSalesContractLogic.ReadByIdAsync(Convert.ToInt32(model.SalesContractId));
                    if (dataFPSalesContract != null)
                    {
                        dataFPSalesContract.RemainingQuantity = dataFPSalesContract.RemainingQuantity + model.OrderQuantity;
                        this.finishingPrintingSalesContractLogic.UpdateAsync(Convert.ToInt32(dataFPSalesContract.Id), dataFPSalesContract);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        private void ProductionOrderNumberGenerator(ProductionOrderModel model, int index)
        {
            ProductionOrderModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.OrderTypeName.Equals(model.OrderTypeName)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            string DocumentType = model.OrderTypeName.ToLower().Equals("printing") ? "P" : "F";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;

            if (lastData == null)
            {
                model.AutoIncreament = 1 + index;
                model.OrderNo = $"{DocumentType}/{YearNow}/0001";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.OrderNo = $"{DocumentType}/{YearNow}/0001";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.OrderNo = $"{DocumentType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(4, '0')}";
                }
            }
        }
    }
}
