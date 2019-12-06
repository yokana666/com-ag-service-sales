using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationDataUtils : BaseDataUtil<FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationModel>
    {
        public FinishingPrintingCostCalculationDataUtils(FinishingPrintingCostCalculationFacade facade) : base(facade)
        {
        }

        public override Task<FinishingPrintingCostCalculationModel> GetNewData()
        {
            return Task.FromResult(new FinishingPrintingCostCalculationModel()
            {
                ActualPrice = 1,
                BuyerName = "name",
                CargoCost = 1,
                Code = "code",
                CurrencyRate = 1,
                Date = DateTimeOffset.UtcNow,
                GreigeId = 1,
                GreigeName = "name",
                InstructionId = 1,
                InstructionName ="name",
                InsuranceCost = 1,
                PreparationFabricWeight = 1,
                PreparationValue = 1,
                ProductionOrderId = 1,
                ProductionOrderNo = "np",
                ProductionUnitValue = 1,
                Remark = "r",
                TKLQuantity = 1,
                RFDFabricWeight = 1,
                Machines = new List<FinishingPrintingCostCalculationMachineModel>()
                {
                    new FinishingPrintingCostCalculationMachineModel()
                    {
                        Chemicals = new List<FinishingPrintingCostCalculationChemicalModel>()
                        {
                            new FinishingPrintingCostCalculationChemicalModel()
                            {
                                ChemicalId = 1,
                                ChemicalQuantity = 1,
                                Price = 1
                            }
                        },
                        Depretiation = 1,
                        MachineId = 1,
                        StepProcessId = 1,
                        Total = 2,
                        Utility = 1
                    }
                }
            });
        }
    }
}
