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
        
        
        public DateTimeOffset Date { get; set; }
        
        public long GreigeId { get; set; }
        
        [MaxLength(1024)]
        public string GreigeName { get; set; }

        public decimal GreigePrice { get; set; }

        public decimal CurrencyRate { get; set; }
        
        public double ProductionUnitValue { get; set; }
        
        public int TKLQuantity { get; set; }

        public double PreparationFabricWeight { get; set; }
        
        public double RFDFabricWeight { get; set; }
        
        public decimal ActualPrice { get; set; }
        
        public decimal CargoCost { get; set; }

        public decimal FreightCost { get; set; }

        public double InsuranceCost { get; set; }
        
        [MaxLength(4096)]
        public string Remark { get; set; }
        
        public long PreSalesContractId { get; set; }

        [MaxLength(64)]
        public string PreSalesContractNo { get; set; }

        public int UnitId { get; set; }

        [MaxLength(512)]
        public string UnitName { get; set; }

        public long UomId { get; set; }

        [MaxLength(128)]
        public string UomUnit { get; set; }

        public double OrderQuantity { get; set; }

        public long MaterialId { get; set; }

        [MaxLength(1024)]
        public string MaterialName { get; set; }

        [MaxLength(256)]
        public string Color { get; set; }

        public long SalesId { get; set; }
        
        [MaxLength(1024)]
        public string SalesUserName { get; set; }

        [MaxLength(1024)]
        public string SalesFirstName { get; set; }

        [MaxLength(1024)]
        public string SalesLastName { get; set; }
        public decimal ConfirmPrice { get; set; }
        public double Comission { get; set; }

        public ICollection<FinishingPrintingCostCalculationMachineModel> Machines { get; set; }
        
        [MaxLength(16)]
        public string Code { get; set; }

        public bool IsPosted { get; set; }
    }
}
