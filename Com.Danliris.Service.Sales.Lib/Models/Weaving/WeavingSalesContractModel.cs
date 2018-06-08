using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.Weaving
{
    public class WeavingSalesContractModel : BaseModel
    {
        [MaxLength(255)]
        public string SalesContractNo { get; set; }
        [MaxLength(255)]
        public string DispositionNumber { get; set; }
        public bool FromStock { get; set; }
        [MaxLength(255)]
        public string MaterialWidth { get; set; }
        public double OrderQuantity { get; set; }
        public double ShippingQuantityTolerance { get; set; }
        public string ComodityDescription { get; set; }
        [MaxLength(255)]
        public string IncomeTax { get; set; }
        [MaxLength(1000)]
        public string TermOfShipment { get; set; }
        [MaxLength(1000)]
        public string TransportFee { get; set; }
        [MaxLength(1000)]
        public string Packing { get; set; }
        [MaxLength(1000)]
        public string DeliveredTo { get; set; }
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Comission { get; set; }
        public DateTimeOffset DeliverySchedule { get; set; }
        public string ShipmentDescription { get; set; }
        [MaxLength(1000)]
        public string Condition { get; set; }
        public string Remark { get; set; }
        [MaxLength(1000)]
        public string PieceLength { get; set; }


        /*buyer*/
        public long BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(1000)]
        public string BuyerName { get; set; }
        [MaxLength(255)]
        public string BuyerType { get; set; }

        /*Product material*/
        public long ProductId { get; set; }
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(1000)]
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        [MaxLength(255)]
        public string ProductTags { get; set; }

        /*Uom*/
        public long UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }

        /*material construction*/
        public long MaterialConstructionId { get; set; }
        [MaxLength(1000)]
        public string MaterialConstructionName { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionCode { get; set; }
        public string MaterialConstructionRemark { get; set; }

        /*yarn material*/
        public long YarnMaterialId { get; set; }
        [MaxLength(1000)]
        public string YarnMaterialName { get; set; }
        [MaxLength(255)]
        public string YarnMaterialCode { get; set; }
        public string YarnMaterialRemark { get; set; }

        /*Comodity*/
        public long ComodityId { get; set; }
        [MaxLength(255)]
        public string ComodityCode { get; set; }
        [MaxLength(1000)]
        public string ComodityName { get; set; }
        [MaxLength(255)]
        public string ComodityType { get; set; }

        /*Quality*/
        public long QualityId { get; set; }
        [MaxLength(255)]
        public string QualityCode { get; set; }
        [MaxLength(1000)]
        public string QualityName { get; set; }

        /*TermPayment*/
        public long TermPaymentId { get; set; }
        [MaxLength(255)]
        public string TermPaymentCode { get; set; }
        [MaxLength(1000)]
        public string TermPaymentName { get; set; }
        public bool TermPaymentIsExport { get; set; }

        /*AccountBank*/
        public long AccountBankId { get; set; }
        [MaxLength(255)]
        public string AccountBankCode { get; set; }
        [MaxLength(1000)]
        public string AccountBankName { get; set; }
        public int AccountBankNumber { get; set; }
        [MaxLength(255)]
        public string BankName { get; set; }

        /*Agent*/
        public long AgentId { get; set; }
        [MaxLength(1000)]
        public string AgentName { get; set; }
        [MaxLength(255)]
        public string AgentCode { get; set; }

    }
}
