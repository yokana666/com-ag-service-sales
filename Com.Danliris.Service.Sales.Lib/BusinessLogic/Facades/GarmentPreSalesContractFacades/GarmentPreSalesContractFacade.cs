using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.DanLiris.Service.Purchasing.Lib.Interfaces;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades
{
    public class GarmentPreSalesContractFacade : IGarmentPreSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentPreSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentPreSalesContractLogic garmentPreSalesContractLogic;

        public GarmentPreSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentPreSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentPreSalesContractLogic = serviceProvider.GetService<GarmentPreSalesContractLogic>();
        }

        public async Task<int> CreateAsync(GarmentPreSalesContract model)
        {
            garmentPreSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentPreSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentPreSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentPreSalesContract> ReadByIdAsync(int id)
        {
            return await garmentPreSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentPreSalesContract model)
        {
            garmentPreSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await garmentPreSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}