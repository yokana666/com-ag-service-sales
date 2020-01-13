using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesReceipt
{
    public class SalesReceiptDetailLogic : BaseLogic<SalesReceiptDetailModel>
    {
        public SalesReceiptDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<SalesReceiptDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesReceiptDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesReceiptDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesReceiptDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","SalesInvoiceId","SalesInvoiceNo","DueDate","CurrencyCode","UseVat","TotalAmount","Paid","Nominal","Unpaid","LastModifiedUtc"
            };

            Query = Query
                .Select(field => new SalesReceiptDetailModel
                {
                    Id = field.Id,
                    SalesInvoiceId = field.SalesInvoiceId,
                    SalesInvoiceNo = field.SalesInvoiceNo,
                    DueDate = field.DueDate,
                    CurrencyId = field.CurrencyId,
                    CurrencyCode = field.CurrencyCode,
                    CurrencySymbol = field.CurrencySymbol,
                    CurrencyRate = field.CurrencyRate,
                    UseVat = field.UseVat,
                    TotalAmount = field.TotalAmount,
                    Paid = field.Paid,
                    Nominal = field.Nominal,
                    Unpaid = field.Unpaid,
                    LastModifiedUtc = field.LastModifiedUtc,
                }) ;

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesReceiptDetailModel>.Order(Query, OrderDictionary);

            Pageable<SalesReceiptDetailModel> pageable = new Pageable<SalesReceiptDetailModel>(Query, page - 1, size);
            List<SalesReceiptDetailModel> data = pageable.Data.ToList<SalesReceiptDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesReceiptDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.SalesReceiptModel.Id == id).Select(d => d.Id));
        }
    }
}
