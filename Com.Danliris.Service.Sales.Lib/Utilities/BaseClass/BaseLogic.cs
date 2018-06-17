using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;

namespace Com.Danliris.Service.Sales.Lib.Utilities.BaseClass
{
    public abstract class BaseLogic<TModel> : IBaseLogic<TModel>
       where TModel : BaseModel
    {
        protected DbSet<TModel> DbSet;
        protected IdentityService IdentityService;

        public BaseLogic(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbSet = dbContext.Set<TModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
        }

        public abstract Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter);

        public virtual void Create(TModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public virtual Task<TModel> ReadByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public virtual void Update(int id, TModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public virtual async Task DeleteAsync(int id)
        {
            TModel model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }
    }
}
