using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationMachineViewModel : BaseViewModel
    {
        //public FinishingPrintingCostCalculationMachineViewModel()
        //{

        //}

        //public FinishingPrintingCostCalculationMachineViewModel(FinishingPrintingCostCalculationMachineModel model, List<FinishingPrintingCostCalculationChemicalModel> costCalculationChemicals)
        //{
        //    Id = model.Id;
        //    MachineId = model.MachineId;
        //    StepProcessId = model.StepProcessId;
        //    Chemicals = costCalculationChemicals.Where(entity => entity.CostCalculationMachineId == model.Id).OrderBy(entity => entity.Index).Select(entity => new FinishingPrintingCostCalculationChemicalViewModel(entity)).ToList();
        //}
        public long CostCalculationId { get; set; }
        public int MachineId { get; set; }
        public int StepProcessId { get; set; }
        public decimal Total { get; set; }
        public decimal Utility { get; set; }
        public decimal Depretiation { get; set; }
        public List<FinishingPrintingCostCalculationChemicalViewModel> Chemicals { get; set; }
        public int Index { get; set; }
        
    }
}
