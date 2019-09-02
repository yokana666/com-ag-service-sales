using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ROGarmentInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics;
using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Sales.Lib.Helpers;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ROGarment
{
    public class ROGarmentFacade : IROGarment
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<RO_Garment> DbSet;
        private readonly IdentityService identityService;
        private readonly ROGarmentLogic roGarmentLogic;
        private readonly ICostCalculationGarment costCalGarmentLogic;
        public IServiceProvider ServiceProvider;

        public ROGarmentFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<RO_Garment>();
            identityService = serviceProvider.GetService<IdentityService>();
            roGarmentLogic = serviceProvider.GetService<ROGarmentLogic>();
            costCalGarmentLogic = serviceProvider.GetService<ICostCalculationGarment>();
            ServiceProvider = serviceProvider;
        }
        private IAzureImageFacade AzureImageFacade
        {
            get { return this.ServiceProvider.GetService<IAzureImageFacade>(); }
        }

        public async Task<int> CreateAsync(RO_Garment Model)
        {
            do
            {
                Model.Code = Code.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(Model.Code)));

            CostCalculationGarment costCalculationGarment = await costCalGarmentLogic.ReadByIdAsync((int)Model.CostCalculationGarment.Id); //Model.CostCalculationGarment;
            foreach(var item in costCalculationGarment.CostCalculationGarment_Materials)
            {
                foreach(var itemModel in Model.CostCalculationGarment.CostCalculationGarment_Materials)
                {
                    if(item.Id == itemModel.Id)
                    {
                        item.Information = itemModel.Information;
                    }
                }
            }
            Model.CostCalculationGarment = null;

            Model.ImagesPath = await this.AzureImageFacade.UploadMultipleImage(Model.GetType().Name, (int)Model.Id, Model.CreatedUtc, Model.ImagesFile, Model.ImagesPath);
            roGarmentLogic.Create(Model);
            int created = await DbContext.SaveChangesAsync();
                        
            await UpdateCostCalAsync(costCalculationGarment, (int)Model.Id);
            return created;
        }

        public async Task<int> UpdateCostCalAsync(CostCalculationGarment costCalculationGarment, int Id)
        {
            costCalculationGarment.RO_GarmentId = Id;
            await costCalGarmentLogic.UpdateAsync((int)costCalculationGarment.Id, costCalculationGarment);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            RO_Garment deletedImage = await this.ReadByIdAsync(id);
            await this.AzureImageFacade.RemoveMultipleImage(deletedImage.GetType().Name, deletedImage.ImagesPath);

            await roGarmentLogic.DeleteAsync(id);
            int deleted = await DbContext.SaveChangesAsync();

            await DeletedROCostCalAsync(deletedImage.CostCalculationGarment, (int)deletedImage.CostCalculationGarmentId);
            return deleted;
        }

        public async Task<int> DeletedROCostCalAsync(CostCalculationGarment costCalculationGarment, int Id)
        {
            CostCalculationGarment costCal= await costCalGarmentLogic.ReadByIdAsync((int)costCalculationGarment.Id); //Model.CostCalculationGarment;

            costCal.RO_GarmentId = null;
            costCal.ImageFile = string.IsNullOrWhiteSpace(costCal.ImageFile) ? "#" : costCal.ImageFile;
            foreach(var item in costCal.CostCalculationGarment_Materials)
            {
                item.Information = null;
            }
            await costCalGarmentLogic.UpdateAsync((int)costCal.Id, costCal);

            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<RO_Garment> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return roGarmentLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<RO_Garment> ReadByIdAsync(int id)
        {
            RO_Garment read = await this.DbSet
                .Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
                .Include(d => d.RO_Garment_SizeBreakdowns)
                    .ThenInclude(sb => sb.RO_Garment_SizeBreakdown_Details)
                .Include(d => d.CostCalculationGarment)
                    .ThenInclude(ccg => ccg.CostCalculationGarment_Materials)
                .FirstOrDefaultAsync();

            read.CostCalculationGarment.ImageFile = await this.AzureImageFacade.DownloadImage(read.CostCalculationGarment.GetType().Name, read.CostCalculationGarment.ImagePath);
            read.ImagesFile = await this.AzureImageFacade.DownloadMultipleImages(read.GetType().Name, read.ImagesPath);

            return read;
        }

        public async Task<int> UpdateAsync(int id, RO_Garment Model)
        {
            CostCalculationGarment costCalculationGarment = Model.CostCalculationGarment;
            Model.CostCalculationGarment = null;

            Model.ImagesPath = await this.AzureImageFacade.UploadMultipleImage(Model.GetType().Name, (int)Model.Id, Model.CreatedUtc, Model.ImagesFile, Model.ImagesPath);

            roGarmentLogic.UpdateAsync(id,Model);
            int updated = await DbContext.SaveChangesAsync();

            await UpdateCostCalAsync(costCalculationGarment, (int)Model.Id);
            return updated;
        }

        public async Task<int> PostRO(List<long> listId)
        {
            int Updated = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    roGarmentLogic.PostRO(listId);
                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
            return Updated;
        }

        public async Task<int> UnpostRO(long id)
        {
            int Updated = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    roGarmentLogic.UnpostRO(id);
                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
            return Updated;
        }
    }
}
