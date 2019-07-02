using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel
{
    public class GarmentPreSalesContract : BaseModel
    {
        [MaxLength(255)]
        public string SCNo { get; set; }
        public DateTimeOffset SCDate { get; set; }
        public string SCType { get; set; }
        public int SectionId { get; set; }
        public string SectionCode { get; set; }
        public int BuyerAgentId { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public int BuyerBrandId { get; set; }
        public string BuyerBrandCode { get; set; }
        public string BuyerBrandName { get; set; }
        public int OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsCC { get; set; }
        public bool IsPR { get; set; }
    }
}