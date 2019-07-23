using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.ROGarments
{
	public class RO_Garment : BaseModel
    {
		public long CostCalculationGarmentId { get; set; }
        [ForeignKey("CostCalculationGarmentId")]
        public virtual CostCalculationGarment CostCalculationGarment { get; set; }
		public string Code { get; set; }
		public ICollection<RO_Garment_SizeBreakdown> RO_Garment_SizeBreakdowns { get; set; }
		public string Instruction { get; set; }
		public int Total { get; set; }
		public List< string> ImagesFile { get; set; }
		public string ImagesPath { get; set; }
		public string ImagesName { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //	//RO_GarmentService service = validationContext.GetService<RO_GarmentService>();

        //	//if (service.DbSet.Count(ro => ro.Id != this.Id && ro.CostCalculationGarmentId.Equals(this.CostCalculationGarmentId) && ro._IsDeleted.Equals(false)) > 0)
        //	//	yield return new ValidationResult("Cost Calculation Garment telah terdaftar di RO", new List<string> { "CostCalculationGarment" });
        //	return new List<ValidationResult>();
        //}
    }
}
