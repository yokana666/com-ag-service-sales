using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels
{
    public class MaxWHConfirmViewModel : BaseViewModel, IValidatableObject
    {
        public double MaxValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxValue <=0)
            {
                yield return new ValidationResult("Max WH Confirm tidak boleh kosong", new List<string> { "wh" });
            }
        }
    }
}
