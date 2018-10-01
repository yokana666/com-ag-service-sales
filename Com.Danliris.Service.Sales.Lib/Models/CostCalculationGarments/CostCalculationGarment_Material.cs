using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments
{
	public class CostCalculationGarment_Material : BaseModel 
	{
		public long CostCalculationGarmentId { get; set; }
        [ForeignKey("CostCalculationGarmentId")]
        public virtual CostCalculationGarment CostCalculationGarment { get; set; }
		public string Code { get; set; }
		public string PO_SerialNumber { get; set; }
		public string PO { get; set; }
		public string CategoryId { get; set; }
		public string CategoryCode { get; set; }
		public string CategoryName { get; set; }
		//public int MaterialId { get; set; }
		//public string MaterialName { get; set; }
		public int AutoIncrementNumber { get; set; }
		public string ProductId { get; set; }
		public string ProductCode { get; set; }
		public string Composition { set; get; }
		public string Construction { set; get; }
		public string Yarn { set; get; }
		public string Width { set; get; }
		public string Description { get; set; }
		public double Quantity { get; set; }
		public string UOMQuantityId { get; set; }
		public string UOMQuantityName { get; set; }
		public double Price { get; set; }
		public string UOMPriceId { get; set; }
		public string UOMPriceName { get; set; }
		public double Conversion { get; set; }
		public double Total { get; set; }
		public bool isFabricCM { get; set; }
		public double? CM_Price { get; set; }
		public double ShippingFeePortion { get; set; }
		public double TotalShippingFee { get; set; }
		public double BudgetQuantity { get; set; }
		public string Information { get; set; }
		public bool IsPosted { get; set; }
	}
}
