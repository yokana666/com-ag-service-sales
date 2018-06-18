using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting
{
    public class FinishingPrintingSalesContractFacade : IFinishingPrintingSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingSalesContractModel> DbSet;
        private readonly IdentityService identityService;
        private readonly FinishingPrintingSalesContractLogic finishingPrintingSalesContractLogic;

        public FinishingPrintingSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FinishingPrintingSalesContractModel>();
            identityService = serviceProvider.GetService<IdentityService>();
            finishingPrintingSalesContractLogic = serviceProvider.GetService<FinishingPrintingSalesContractLogic>();
        }

        public async Task<int> CreateAsync(FinishingPrintingSalesContractModel model)
        {
            finishingPrintingSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await finishingPrintingSalesContractLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<FinishingPrintingSalesContractModel>, int, Dictionary<string, string>, List<string>> Read(int Page, int Size, string Order, List<string> Select, string Keyword, string Filter)
        {
            return finishingPrintingSalesContractLogic.Read(Page, Size, Order, Select, Keyword, Filter);
        }
        public async Task<FinishingPrintingSalesContractModel> ReadByIdAsync(int id)
        {
            return await finishingPrintingSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, FinishingPrintingSalesContractModel model)
        {
            finishingPrintingSalesContractLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
