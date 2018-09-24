using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class GarmentProductViewModel : BaseViewModel, IValidatableObject
	{
		public string code { get; set; }
		public string composition { get; set; }
		public string construction { get; set; }
		public string yarn { get; set; }
		public string width { get; set; }
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			throw new NotImplementedException();
		}
	}
}
