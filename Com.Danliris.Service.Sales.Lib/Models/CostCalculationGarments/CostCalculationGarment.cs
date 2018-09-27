//using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments
{
	public class CostCalculationGarment : BaseModel
	{
		public string Code { get; set; }
		public string RO_Number { get; set; }
		//public string RO { get; set; }
		public string Article { get; set; }
		public string ComodityID { get; set; }
		public string ComodityCode { get; set; }
		public string Commodity { get; set; }
		public string CommodityDescription { get; set; }
		public double FabricAllowance { get; set; }
		public double AccessoriesAllowance { get; set; }
		public string Section { get; set; }
		public string UOMID { get; set; }
		public string UOMCode { get; set; }
		public string UOMUnit { get; set; }
		public int Quantity { get; set; }
		public string SizeRange { get; set; }
		public DateTimeOffset DeliveryDate { get; set; }
		public DateTimeOffset ConfirmDate { get; set; }
		public int LeadTime { get; set; }
		public double SMV_Cutting { get; set; }
		public double SMV_Sewing { get; set; }
		public double SMV_Finishing { get; set; }
		public double SMV_Total { get; set; }
		public string BuyerId { get; set; }
		public string BuyerCode{ get; set; }
		public string BuyerName { get; set; }
		public int EfficiencyId { get; set; }
		public double EfficiencyValue { get; set; }
		public double Index { get; set; }
		public int WageId { get; set; }
		public double WageRate { get; set; }
		public int THRId { get; set; }
		public double THRRate { get; set; }
		public double ConfirmPrice { get; set; }
		public int RateId { get; set; }
		public double RateValue { get; set; }
		public ICollection<CostCalculationGarment_Material> CostCalculationGarment_Materials { get; set; }
		public double Freight { get; set; }
		public double Insurance { get; set; }
		public double CommissionPortion { get; set; }
		public double CommissionRate { get; set; }
		public int OTL1Id { get; set; }
		public double OTL1Rate { get; set; }
		public double OTL1CalculatedRate { get; set; }
		public int OTL2Id { get; set; }
		public double OTL2Rate { get; set; }
		public double OTL2CalculatedRate { get; set; }
		public double Risk { get; set; }
		public double ProductionCost { get; set; }
		public double NETFOB { get; set; }
		public double FreightCost { get; set; }
		public double NETFOBP { get; set; }
		public string Description { get; set; }
		public string ImageFile { get; set; }
		public string ImagePath { get; set; }
		public int? RO_GarmentId { get; set; }
		public int UnitId { get; set; }
		public string UnitCode { get; set; }
		public string UnitName { get; set; }
		public int AutoIncrementNumber { get; set; }
        //[ForeignKey("RO_GarmentId")]
        //public virtual RO_Garment RO_Garment { get; set; }

        public int BuyerBrandId { get; set; }
        public string BuyerBrandCode { get; set; }
        public string BuyerBrandName { get; set; }
    }
}
