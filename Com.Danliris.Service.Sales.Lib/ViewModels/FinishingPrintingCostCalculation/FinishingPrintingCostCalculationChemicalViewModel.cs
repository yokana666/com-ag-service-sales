using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationChemicalViewModel : BaseViewModel
    {
        //public FinishingPrintingCostCalculationChemicalViewModel()
        //{

        //}

        //public FinishingPrintingCostCalculationChemicalViewModel(FinishingPrintingCostCalculationChemicalModel entity)
        //{
        //    Id = entity.Id;
        //    ChemicalId = entity.ChemicalId;
        //    ChemicalQuantity = entity.ChemicalQuantity;
        //}
        public long CostCalculationId { get; set; }
        public long CostCalculationMachineId { get; set; }
        
        public int ChemicalId { get; set; }
        public int ChemicalQuantity { get; set; }
        public decimal Price { get; set; }
        //public int Index { get; set; }
    }
}
