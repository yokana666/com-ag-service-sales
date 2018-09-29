using Com.Danliris.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics
{
    public class GarmentSalesContractLogic : BaseLogic<GarmentSalesContract>
    {
        private GarmentSalesContractItemLogic GarmentSalesContractItemLogic;
        private readonly SalesDbContext DbContext;
        public GarmentSalesContractLogic(GarmentSalesContractItemLogic garmentSalesContractItemLogic, IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.GarmentSalesContractItemLogic = garmentSalesContractItemLogic;
            this.DbContext = dbContext;
        }

        public override ReadResponse<GarmentSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentSalesContract> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo", "RONumber", "Article", "Comodity","BuyerName"
            };

            Query = QueryHelper<GarmentSalesContract>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentSalesContract>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "BuyerName", "SalesContractNo", "LastModifiedUtc","CreatedUtc","Comodity","Article","RONumber"
            };

            Query = Query
                .Select(sc => new GarmentSalesContract
                {
                    Id = sc.Id,
                    RONumber=sc.RONumber,
                    SalesContractNo = sc.SalesContractNo,
                    BuyerName = sc.BuyerName,
                    BuyerId = sc.BuyerId,
                    CreatedUtc= sc.CreatedUtc,
                    LastModifiedUtc = sc.LastModifiedUtc,
                    AccountBankId=sc.AccountBankId,
                    AccountBankName=sc.AccountBankName,
                    AccountName=sc.AccountName,
                    Amount=sc.Amount,
                    IsDeleted=sc.IsDeleted,
                    IsEmbrodiary=sc.IsEmbrodiary,
                    Comodity=sc.Comodity,
                    Article=sc.Article,
                    DocPrinted=sc.DocPrinted
                }).OrderByDescending(s=>s.LastModifiedUtc);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentSalesContract>.Order(Query, OrderDictionary);

            Pageable<GarmentSalesContract> pageable = new Pageable<GarmentSalesContract>(Query, page - 1, size);
            List<GarmentSalesContract> data = pageable.Data.ToList<GarmentSalesContract>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentSalesContract>(data, totalData, OrderDictionary, SelectedFields);
        }

        public async override void Create(GarmentSalesContract model)
        {
            GenerateNo(model);
            if(model.Items.Count>0)
                foreach (var detail in model.Items)
                {
                    GarmentSalesContractItemLogic.Create(detail);
                    //EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
                }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task<GarmentSalesContract> ReadByIdAsync(int id)
        {
            var garmentSalesContract = await DbSet.Include(p => p.Items).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            garmentSalesContract.Items = garmentSalesContract.Items.OrderBy(s => s.Id).ToArray();
            return garmentSalesContract;
        }

        public override async void UpdateAsync(int id, GarmentSalesContract model)
        {
            if (model.Items != null)
            {
                HashSet<long> itemIds = GarmentSalesContractItemLogic.GetGSalesContractIds(id);
                if (itemIds.Count.Equals(0))
                {
                    foreach (var detail in model.Items)
                    {
                        GarmentSalesContractItemLogic.Create(detail);
                        //EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
                    }
                }
                //else if (model.Items.Count.Equals(0))
                //{
                //    foreach (var oldItem in itemIds)
                //    {
                //        GarmentSalesContractItem dataItem = DbContext.GarmentSalesContractItems.FirstOrDefault(prop => prop.Id.Equals(oldItem));
                //        EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                //    }
                //}
                else
                {
                    foreach (var itemId in itemIds)
                    {

                        GarmentSalesContractItem data = model.Items.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                        {
                            GarmentSalesContractItem dataItem = DbContext.GarmentSalesContractItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                            EntityExtension.FlagForDelete(dataItem, IdentityService.Username, "sales-service");
                        }
                            //await GarmentSalesContractItemLogic.DeleteAsync(Convert.ToInt32(itemId));
                        else
                        {
                            //GarmentSalesContractItem dataItem = DbContext.GarmentSalesContractItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                            //EntityExtension.FlagForUpdate(dataItem, IdentityService.Username, "sales-service");
                            GarmentSalesContractItemLogic.UpdateAsync(Convert.ToInt32(itemId), data);
                        }

                        foreach (GarmentSalesContractItem item in model.Items)
                        {
                            if (item.Id == 0)
                                GarmentSalesContractItemLogic.Create(item);
                        }
                    }

                }
                
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(int id)
        {
            var model = await ReadByIdAsync(id);

            foreach (var item in model.Items)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service");
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        private void GenerateNo(GarmentSalesContract model)
        {
            string Year = model.CreatedUtc.ToString("yy");
            string Month = model.CreatedUtc.ToString("MM");

            string no = $"{model.ComodityCode}/SC/DL/{Year}";
            int Padding = 5;

            var lastData = DbSet.IgnoreQueryFilters().Where(w=>w.SalesContractNo.StartsWith(no) && !w.IsDeleted).OrderByDescending(o => o.CreatedUtc).FirstOrDefault();

            //string DocumentType = model.BuyerType.ToLower().Equals("ekspor") || model.BuyerType.ToLower().Equals("export") ? "FPE" : "FPL";

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;

            if (lastData == null)
            {
                model.SalesContractNo = no + "1".PadLeft(Padding, '0');
            }
            else
            {
                int lastNoNumber = Int32.Parse(lastData.SalesContractNo.Replace(no, "")) + 1;
                model.SalesContractNo = no + lastNoNumber.ToString().PadLeft(Padding, '0');
            }
            
        }

        //async Task<string> GenerateNo(GarmentSalesContract model)
        //{
        //    string Year = model.CreatedUtc.ToString("yy");
        //    string Month = model.CreatedUtc.ToString("MM");

        //    string no = $"{model.ComodityCode}/SC/DL/{Year}/";
        //    int Padding = 4;

        //    var lastNo = await DbContext.GarmentSalesContracts.Where(w => w.SalesContractNo.StartsWith(no) && !w.IsDeleted).OrderByDescending(o => o.CreatedUtc).FirstOrDefaultAsync();

        //    if (lastNo == null)
        //    {
        //        return no + "1".PadLeft(Padding, '0');
        //    }
        //    else
        //    {
        //        int lastNoNumber = Int32.Parse(lastNo.SalesContractNo.Replace(no, "")) + 1;
        //        return no + lastNoNumber.ToString().PadLeft(Padding, '0');
        //    }
        //}
    }
}
