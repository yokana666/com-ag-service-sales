using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceModel : BaseModel
    {
        [MaxLength(25)]
        public string Code { get; set; }
        [MaxLength(25)]
        public string SalesInvoiceNo { get; set; }
        public DateTimeOffset SalesInvoiceDate { get; set; }
        [MaxLength(25)]
        public string DeliveryOrderNo { get; set; }

        /*DO Sales*/
        public int DOSalesId { get; set; }
        [MaxLength(25)]
        public string DOSalesNo { get; set; }

        /*Buyer*/
        public int BuyerId { get; set; }
        [MaxLength(250)]
        public string BuyerName { get; set; }
        [MaxLength(100)]
        public string BuyerNPWP { get; set; }

        /*Currency*/
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }


        [MaxLength(100)]
        public string NPWP { get; set; }
        [MaxLength(100)]
        public string NPPKP { get; set; }
        [MaxLength(25)]
        public string DebtorIndexNo { get; set; }
        public DateTimeOffset DueDate { get; set; }
        [MaxLength(25)]
        public string Disp { get; set; }
        [MaxLength(25)]
        public string Op { get; set; }
        [MaxLength(25)]
        public string Sc { get; set; }
        public bool UseVat { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }


        public virtual ICollection<SalesInvoiceDetailModel> SalesInvoiceDetails { get; set; }
    }
}
