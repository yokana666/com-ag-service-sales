using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacade : IFinishingPrintingCostCalculationService
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingCostCalculationModel> DbSet;
        private readonly FinishingPrintingCostCalculationLogic finishingPrintingCostCalculationLogic;
        private readonly DbSet<FinishingPrintingCostCalculationChemicalModel> ChemicalDBSet;


        public FinishingPrintingCostCalculationFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FinishingPrintingCostCalculationModel>();
            ChemicalDBSet = DbContext.Set<FinishingPrintingCostCalculationChemicalModel>();
            finishingPrintingCostCalculationLogic = serviceProvider.GetService<FinishingPrintingCostCalculationLogic>();
        }

        public async Task<int> CreateAsync(FinishingPrintingCostCalculationModel model)
        {
            int created = 0;
            finishingPrintingCostCalculationLogic.Create(model);
            created += await DbContext.SaveChangesAsync();

            var chemicalModels = model.Machines.ToList().SelectMany(entity =>
            {
                var result = entity.Chemicals.ToList().Select(chemical =>
                {
                    chemical.CostCalculationId = model.Id;
                    return chemical;
                });
                return result;
            });

            ChemicalDBSet.UpdateRange(chemicalModels);
            created += await DbContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await finishingPrintingCostCalculationLogic.DeleteAsync(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CCPost(List<long> listId)
        {
            await finishingPrintingCostCalculationLogic.CCPost(listId);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<FinishingPrintingCostCalculationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return finishingPrintingCostCalculationLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<FinishingPrintingCostCalculationModel> ReadByIdAsync(int id)
        {
            return await finishingPrintingCostCalculationLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, FinishingPrintingCostCalculationModel model)
        {
            finishingPrintingCostCalculationLogic.UpdateAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<FinishingPrintingCostCalculationModel> ReadParent(long id)
        {
            return await finishingPrintingCostCalculationLogic.ReadParent(id);
        }
    }
}
