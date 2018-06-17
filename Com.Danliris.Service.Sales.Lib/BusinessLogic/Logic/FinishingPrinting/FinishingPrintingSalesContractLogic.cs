using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class FinishingPrintingSalesContractLogic : BaseLogic<FinishingPrintingSalesContractModel>
    {
        public FinishingPrintingSalesContractLogic(IServiceProvider serviceProvider, SalesDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<FinishingPrintingSalesContractModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FinishingPrintingSalesContractModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo", "Buyer.Type", "Buyer.Name"
            };

            Query = QueryHelper<FinishingPrintingSalesContractModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Buyer", "DeliverySchedule", "SalesContractNo", "LastModifiedUtc"
            };

            Query = Query
                .Select(field => new FinishingPrintingSalesContractModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    SalesContractNo = field.SalesContractNo,
                    BuyerType = field.BuyerType,
                    BuyerName = field.BuyerName,
                    DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<FinishingPrintingSalesContractModel>.Order(Query, OrderDictionary);

            List<FinishingPrintingSalesContractModel> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = DbSet.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void Create(FinishingPrintingSalesContractModel model)
        {
            SalesContractNumberGenerator(model);
            foreach (var detail in model.Details)
            {
                EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task<FinishingPrintingSalesContractModel> ReadByIdAsync(int id)
        {
            var finishingPrintingSalesContract = await DbSet.Include(p => p.Details).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            finishingPrintingSalesContract.Details = finishingPrintingSalesContract.Details.OrderBy(s => s.Id).ToArray();
            return finishingPrintingSalesContract;
        }

        public override void Update(int id, FinishingPrintingSalesContractModel model)
        {
            foreach (var item in model.Details)
            {
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(int id)
        {
            var model = await ReadByIdAsync(id);

            foreach (var Detail in model.Details)
            {
                EntityExtension.FlagForDelete(Detail, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        private void SalesContractNumberGenerator(FinishingPrintingSalesContractModel model)
        {
            FinishingPrintingSalesContractModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.OrderTypeName.Equals(model.OrderTypeName)).OrderByDescending(o => o.AutoIncrementNumber).FirstOrDefault();

            string DocumentType = model.BuyerType.ToLower().Equals("ekspor") || model.BuyerType.ToLower().Equals("export") ? "FPE" : "FPL";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;

            if (lastData == null)
            {
                model.AutoIncrementNumber = 1;
                model.SalesContractNo = $"0001/{DocumentType}/{MonthNow}/{YearNow}";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncrementNumber = 1;
                    model.SalesContractNo = $"0001/{DocumentType}/{MonthNow}/{YearNow}";
                }
                else
                {
                    model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    model.SalesContractNo = $"{lastData.AutoIncrementNumber.ToString().PadLeft(4, '0')}/{DocumentType}/{MonthNow}/{YearNow}";
                }
            }
        }
    }
}
