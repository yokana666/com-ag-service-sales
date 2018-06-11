using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Weaving;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Weaving;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving
{
    public class WeavingSalesContractFacade : IWeavingSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<WeavingSalesContractModel> DbSet;
        private IdentityService IdentityService;
        private WeavingSalesContractLogic WeavingSalesContractLogic;

        public WeavingSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<WeavingSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.WeavingSalesContractLogic = serviceProvider.GetService<WeavingSalesContractLogic>();
        }

        public async Task<int> Create(WeavingSalesContractModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            WeavingSalesContractLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<WeavingSalesContractModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return WeavingSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<WeavingSalesContractModel> ReadById(int id)
        {
            return await WeavingSalesContractLogic.ReadById(id);
        }

        public async Task<int> Update(int id, WeavingSalesContractModel model)
        {
            WeavingSalesContractLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            await WeavingSalesContractLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}
