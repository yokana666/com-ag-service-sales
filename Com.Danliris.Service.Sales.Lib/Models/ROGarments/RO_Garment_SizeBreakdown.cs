using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.ROGarments
{
	public class RO_Garment_SizeBreakdown : StandardEntity//, IValidatableObject
	{
		public int RO_GarmentId { get; set; }
		public virtual RO_Garment RO_Garment { get; set; }
		public string Code { get; set; }
		public int ColorId { get; set; }
		public string ColorName { get; set; }
		public ICollection<RO_Garment_SizeBreakdown_Detail> RO_Garment_SizeBreakdown_Details { get; set; }
		public int Total { get; set; }

		//public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//	return new List<ValidationResult>();
		//}
	}
}
