using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades
{
    public class WeeklyPlanFacade : IWeeklyPlanFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<WeeklyPlan> DbSet;
        private IdentityService IdentityService;
        private WeeklyPlanLogic WeeklyPlanLogic;

        public WeeklyPlanFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<WeeklyPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            WeeklyPlanLogic = serviceProvider.GetService<WeeklyPlanLogic>();
        }

        public async Task<int> CreateAsync(WeeklyPlan model)
        {
            WeeklyPlanLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await WeeklyPlanLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<WeeklyPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return WeeklyPlanLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<WeeklyPlan> ReadByIdAsync(int id)
        {
            return await WeeklyPlanLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, WeeklyPlan model)
        {
            WeeklyPlanLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public List<string> GetYears(string keyword)
        {
            return WeeklyPlanLogic.GetYears(keyword);
        }
    }
}
