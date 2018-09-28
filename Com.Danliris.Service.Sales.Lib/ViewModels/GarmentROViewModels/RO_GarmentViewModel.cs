using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.GarmentROViewModels
{
    public class RO_GarmentViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public CostCalculationGarmentViewModel CostCalculationGarment { get; set; }
        public List<RO_Garment_SizeBreakdownViewModel> RO_Garment_SizeBreakdowns { get; set; }
        public string Instruction { get; set; }
        public int Total { get; set; }
        public List<string> ImagesFile { get; set; }
        public List<string> ImagesPath { get; set; }
        public List<string> ImagesName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.CostCalculationGarment == null)
                yield return new ValidationResult("Nomor RO harus diisi", new List<string> { "CostCalculationGarment" });

            if (this.RO_Garment_SizeBreakdowns == null || this.RO_Garment_SizeBreakdowns.Count == 0)
                yield return new ValidationResult("Size Breakdown harus diisi", new List<string> { "SizeBreakdowns" });
            else
            {
                int Count = 0;
                string error = "[";

                foreach (var item in this.RO_Garment_SizeBreakdowns)
                {

                    error += " { ";

                    if (item.Color == null)
                    {
                        Count++;
                        error += "Color: 'Color is required', ";
                    }

                    if (item.RO_Garment_SizeBreakdown_Details == null || item.RO_Garment_SizeBreakdown_Details.Count == 0)
                    {
                        Count++;
                        error += "Detail: 'Detail is required', ";
                    }

                    error += " }, ";
                }

                error += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(error, new List<string> { "SizeBreakdownItems" });
                }
            }
        }
    }
}
