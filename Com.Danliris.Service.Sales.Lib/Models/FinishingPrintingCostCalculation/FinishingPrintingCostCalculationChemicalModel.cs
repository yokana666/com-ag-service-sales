using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationChemicalModel : BaseModel
    {
        public long CostCalculationId { get; set; }
        
        public long CostCalculationMachineId { get; set; }
        
        [ForeignKey("CostCalculationMachineId")]
        public virtual FinishingPrintingCostCalculationMachineModel FinishingPrintingCostCalculationMachine { get; set; }
        

        public int Index { get; set; }
        
        public int ChemicalId { get; set; }
        
        public int ChemicalQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
