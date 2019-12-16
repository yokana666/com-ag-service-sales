using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoice
{
    class SalesInvoiceDataUtil : BaseDataUtil<SalesInvoiceFacade, SalesInvoiceModel>
    {
        public SalesInvoiceDataUtil(SalesInvoiceFacade facade) : base(facade)
        {
        }

        public override async Task<SalesInvoiceModel> GetNewData()
        {
            return new SalesInvoiceModel()
            {
                Id = 1,
                Code = "Code",
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DeliveryOrderNo = "DeliveryOrderNo",
                DOSalesId = 1,
                DOSalesNo = "DOSalesNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerNPWP = "BuyerNPWP",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp.",
                NPWP = "NPWP",
                NPPKP = "NPPKP",
                DebtorIndexNo = "DebtorIndexNo",
                DueDate = DateTimeOffset.UtcNow,
                Disp = "Disp",
                Op = "Op",
                Sc = "Sc",
                UseVat = true,
                Notes = "Notes",

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        UnitCode = "UnitCode",
                        Quantity = "Quantity",
                        Total = 1,
                        UomUnit = "PCS",
                        UnitName = "UnitName",
                        UnitPrice = 1,
                        Amount = 100
                    }
                }
            };
        }
    }
}
