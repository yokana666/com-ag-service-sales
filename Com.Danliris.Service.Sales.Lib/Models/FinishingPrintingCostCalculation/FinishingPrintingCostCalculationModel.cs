using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationModel : BaseModel
    {
        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }
        
        public int InstructionId { get; set; }
        
        [MaxLength(128)]
        public string InstructionName { get; set; }
        
        [MaxLength(128)]
        public string BuyerName { get; set; }
        
        public DateTimeOffset Date { get; set; }
        
        public int GreigeId { get; set; }
        
        [MaxLength(128)]
        public string GreigeName { get; set; }
        
        public double PreparationValue { get; set; }
        
        public double CurrencyRate { get; set; }
        
        public double ProductionUnitValue { get; set; }
        
        public int TKLQuantity { get; set; }

        public double PreparationFabricWeight { get; set; }
        
        public double RFDFabricWeight { get; set; }
        
        public double ActualPrice { get; set; }
        
        public double CargoCost { get; set; }
        
        public double InsuranceCost { get; set; }
        
        [MaxLength(2048)]
        public string Remark { get; set; }
        
        public int ProductionOrderId { get; set; }
        
        public ICollection<FinishingPrintingCostCalculationMachineModel> Machines { get; set; }
        
        [MaxLength(16)]
        public string Code { get; set; }
    }
}
