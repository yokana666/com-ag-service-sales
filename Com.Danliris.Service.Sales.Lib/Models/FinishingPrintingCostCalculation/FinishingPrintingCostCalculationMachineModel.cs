using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationMachineModel : BaseModel
    {
        public long CostCalculationId { get; set; }
        
        [ForeignKey("CostCalculationId")]
        public virtual FinishingPrintingCostCalculationModel FinishingPrintingCostCalculation { get; set; }
        
        
        public int Index { get; set; }
        
        public int MachineId { get; set; }
        
        public ICollection<FinishingPrintingCostCalculationChemicalModel> Chemicals { get; set; }
       
        public int StepProcessId { get; set; }
        public decimal Total { get; set; }
        public decimal Utility { get; set; }
        public decimal Depretiation { get; set; }
    }
}
