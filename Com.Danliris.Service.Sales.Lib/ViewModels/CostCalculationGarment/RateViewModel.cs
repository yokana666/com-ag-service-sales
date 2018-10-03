using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class RateViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Nama ongkos harus diisi", new List<string> { "Name" });

            if (this.Value == null)
                yield return new ValidationResult("Tarif ongkos harus diisi", new List<string> { "Value" });
            else if (this.Value <= 0)
                yield return new ValidationResult("Tarif ongkos harus lebih besar dari 0", new List<string> { "Value" });
        }
    }
}
