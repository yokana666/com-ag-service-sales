using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice
{
    public class SalesInvoiceFacade : ISalesInvoiceContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<SalesInvoiceModel> DbSet;
        private readonly IIdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private readonly SalesInvoiceLogic salesInvoiceLogic;
        public SalesInvoiceFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<SalesInvoiceModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            this.salesInvoiceLogic = serviceProvider.GetService<SalesInvoiceLogic>();
            this.identityService = serviceProvider.GetService<IIdentityService>();
        }

        public async Task<int> CreateAsync(SalesInvoiceModel model)
        {
            int result = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 0;
                    foreach (var item in model.SalesInvoiceDetails)
                    {

                        SalesInvoiceModel salesInvoice = new SalesInvoiceModel()
                        {
                            Id = model.Id,
                            Code = model.Code,
                            AutoIncreament = model.AutoIncreament,
                            SalesInvoiceNo = model.SalesInvoiceNo,
                            SalesInvoiceType = model.SalesInvoiceType,
                            SalesInvoiceDate = model.SalesInvoiceDate,
                            DueDate = model.DueDate,
                            DeliveryOrderNo = model.DeliveryOrderNo,
                            DebtorIndexNo = model.DebtorIndexNo,
                            DOSalesId = model.DOSalesId,
                            DOSalesNo = model.DOSalesNo,
                            BuyerId = model.BuyerId,
                            BuyerName = model.BuyerName,
                            BuyerAddress = model.BuyerAddress,
                            BuyerNPWP = model.BuyerNPWP,
                            IDNo = model.IDNo,
                            CurrencyId = model.CurrencyId,
                            CurrencyCode = model.CurrencyCode,
                            CurrencySymbol = model.CurrencySymbol,
                            CurrencyRate = model.CurrencyRate,
                            Disp = model.Disp,
                            Op = model.Op,
                            Sc = model.Sc,
                            UseVat = model.UseVat,
                            Remark = model.Remark,

                            Active = model.Active,
                            CreatedAgent = model.CreatedAgent,
                            CreatedBy = model.CreatedBy,
                            CreatedUtc = model.CreatedUtc,
                            DeletedAgent = model.DeletedAgent,
                            DeletedBy = model.DeletedBy,
                            DeletedUtc = model.DeletedUtc,
                            LastModifiedAgent = model.LastModifiedAgent,
                            LastModifiedBy = model.LastModifiedBy,
                            LastModifiedUtc = model.LastModifiedUtc,
                            UId = model.UId,

                            SalesInvoiceDetails = new List<SalesInvoiceDetailModel>
                            {item}
                        };

                        do
                        {
                            model.Code = CodeGenerator.Generate();
                        }
                        while (DbSet.Any(d => d.Code.Equals(model.Code)));

                        SalesInvoiceNumberGenerator(model, index);
                        salesInvoiceLogic.Create(model);
                        index++;
                    }
                    result = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    SalesInvoiceModel model = await salesInvoiceLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        SalesInvoiceModel salesInvoiceModel = new SalesInvoiceModel();

                        salesInvoiceModel = model;
                        await salesInvoiceLogic.DeleteAsync(id);
                    }
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<SalesInvoiceModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return salesInvoiceLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<SalesInvoiceModel> ReadByIdAsync(int id)
        {
            return await salesInvoiceLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, SalesInvoiceModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    salesInvoiceLogic.UpdateAsync(id, model);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return await DbContext.SaveChangesAsync();
        }

        private void SalesInvoiceNumberGenerator(SalesInvoiceModel model, int index)
        {
            SalesInvoiceModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.SalesInvoiceType.Equals(model.SalesInvoiceType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            int YearNow = DateTime.Now.Year;

            if (lastData == null)
            {
                if (model.SalesInvoiceType == "BPF")
                {
                    index = 28;
                }
                else if (model.SalesInvoiceType == "BPS")
                {
                    index = 8;
                }
                else if (model.SalesInvoiceType == "BPP")
                {
                    index = 98;
                }
                else if(model.SalesInvoiceType == "BRG")
                {
                    index = 14;
                }
                else
                {
                    index = 0;
                }
                model.AutoIncreament = 1 + index;
                model.SalesInvoiceNo = $"{model.SalesInvoiceType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.SalesInvoiceNo = $"{model.SalesInvoiceType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.SalesInvoiceNo = $"{model.SalesInvoiceType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }
    }
}
