using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSalesContractModel;
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

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades
{
    public class GarmentSalesContractFacade : IGarmentSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentSalesContractLogic garmentSalesContractLogic;

        public GarmentSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentSalesContractLogic = serviceProvider.GetService<GarmentSalesContractLogic>();
            
        }

        public async Task<int> CreateAsync(GarmentSalesContract model)
        {
            //do
            //{
            //    model.Code = CodeGenerator.Generate();
            //}
            //while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            garmentSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await garmentSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentSalesContract> ReadByIdAsync(int id)
        {
            return await garmentSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentSalesContract model)
        {
            garmentSalesContractLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePrinted(int id, GarmentSalesContract model)
        {
            //garmentSalesContractLogic.UpdateAsync(id, model);
            model.DocPrinted = true;
            DbSet.Update(model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
