using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";

        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public void ValidateVM()
        {
            var vm = new FinishingPrintingCostCalculationViewModel()
            {
                Remark = "1",
                
                ProductionOrderNo = "ee",
            };
            var response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Instruction = new InstructionViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.UOM = new UomViewModel()
            {
                Id = 1,
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.CurrencyRate = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ProductionUnitValue = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.TKLQuantity = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Greige = new ProductViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PreparationFabricWeight = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.RFDFabricWeight = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ActualPrice = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.CargoCost = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.InsuranceCost = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
                {
                    Chemicals = new List<FinishingPrintingCostCalculationChemicalViewModel>()
                    {
                        new FinishingPrintingCostCalculationChemicalViewModel()
                    }
                }
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Machines = new List<FinishingPrintingCostCalculationMachineViewModel>()
            {
                new FinishingPrintingCostCalculationMachineViewModel()
                {
                    Machine = new MachineViewModel()
                    {
                        Id = 1,
                        Name = "a",
                        Process = "a",
                        LPG = 1,
                        Solar = 1,
                        Steam = 1,
                        Water = 1,
                        Electric = 1,
                    },
                    Step = new StepViewModel()
                    {
                        Id = 1,
                        Process = "a"
                    },
                    Depretiation = 1,
                    Index = 1,
                    CostCalculationId = 1,
                    
                    Chemicals = new List<FinishingPrintingCostCalculationChemicalViewModel>()
                    {
                        new FinishingPrintingCostCalculationChemicalViewModel()
                        {
                            Chemical = new ProductViewModel()
                            {
                                Id = 1,
                                Price = 1,
                                Name = "a"
                            },
                            ChemicalQuantity = 1,
                            CostCalculationId = 1,
                            CostCalculationMachineId = 1
                        }
                    }
                }
            };

            response = vm.Validate(null);
            Assert.Empty(response);
        }
    }
}
