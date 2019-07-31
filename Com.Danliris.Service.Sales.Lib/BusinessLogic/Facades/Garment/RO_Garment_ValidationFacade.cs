using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using System.Linq;
using Com.Moonlay.Models;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class RO_Garment_ValidationFacade : IRO_Garment_Validation
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IIdentityService IdentityService;
        private RO_Garment_ValidationLogic RO_Garment_ValidationLogic;

        public RO_Garment_ValidationFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IIdentityService>();
            this.RO_Garment_ValidationLogic = serviceProvider.GetService<RO_Garment_ValidationLogic>();
        }

        public async Task<int> ValidateROGarment(CostCalculationGarment CostCalculationGarment, Dictionary<long, string> productDicts)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = this.DbSet
                        .Include(m => m.CostCalculationGarment_Materials)
                        .FirstOrDefault(m => m.Id == CostCalculationGarment.Id);

                    EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
                    model.IsValidatedROPPIC = true;
                    foreach (var material in model.CostCalculationGarment_Materials)
                    {
                        var sentMaterial = CostCalculationGarment.CostCalculationGarment_Materials.FirstOrDefault(m => m.Id == material.Id);
                        if (sentMaterial != null)
                        {
                            material.IsPosted = true;
                            material.IsPRMaster = sentMaterial.IsPRMaster;
                            EntityExtension.FlagForUpdate(material, IdentityService.Username, "sales-service");
                        }
                    }
                    DbSet.Update(model);

                    Updated = await DbContext.SaveChangesAsync();

                    model.CostCalculationGarment_Materials = model.CostCalculationGarment_Materials
                        .Where(material => CostCalculationGarment.CostCalculationGarment_Materials.Any(oldMaterial => oldMaterial.Id == material.Id) && material.IsPRMaster == false).ToList();

                    if (CostCalculationGarment.CostCalculationGarment_Materials.All(m => !m.CategoryName.ToUpper().Equals("PROCESS")))
                    {
                        await RO_Garment_ValidationLogic.CreateGarmentPurchaseRequest(model, productDicts);
                    }
                    else if (CostCalculationGarment.CostCalculationGarment_Materials.All(m => m.CategoryName.ToUpper().Equals("PROCESS")))
                    {
                        await RO_Garment_ValidationLogic.AddItemsGarmentPurchaseRequest(model, productDicts);
                    }
                    else
                    {
                        throw new Exception("Kategori Ada Proses dan Lainnnya");
                    }

                    transaction.Commit();
                }
                //catch (ServiceValidationException e)
                //{
                //    transaction.Rollback();
                //    throw new ServiceValidationException(e.Message, null, null);
                //}
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Updated;
        }
    }
}
