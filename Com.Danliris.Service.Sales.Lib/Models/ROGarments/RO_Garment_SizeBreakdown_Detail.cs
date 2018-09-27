using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.ROGarments
{
	public class RO_Garment_SizeBreakdown_Detail : BaseModel
    {
		public long RO_Garment_SizeBreakdownId { get; set; }
        [ForeignKey("RO_Garment_SizeBreakdownId")]
        public virtual RO_Garment_SizeBreakdown RO_Garment_SizeBreakdown { get; set; }
		public string Code { get; set; }
		public string Information { get; set; }
		public string Size { get; set; }
		public int Quantity { get; set; }

		//public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//	return new List<ValidationResult>();
		//}
	}
}
