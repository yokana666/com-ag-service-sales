using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingCostCalculation;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Moq;
using Com.Danliris.Service.Sales.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Xunit;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";

        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Create_Printing_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.UnitName = "Printing";
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void CCPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingCostCalculationFacade facade = new FinishingPrintingCostCalculationFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.CCPost(listData);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public void ValidateVM()
        {
            var vm = new FinishingPrintingCostCalculationViewModel()
            {
                Remark = "1",

                ProductionOrderNo = "ee",
                OrderQuantity = 1,
                OTL1 = 1,
                OTL2 = 1,
                Comission = 1,
                IsPosted = false
            };
            var response = vm.Validate(null);
            Assert.NotEmpty(response);
            Assert.NotNull(vm.Remark);
            Assert.NotNull(vm.ProductionOrderNo);
            Assert.NotEqual(0, vm.OrderQuantity);
            Assert.NotEqual(0, vm.OTL2);
            Assert.NotEqual(0, vm.OTL1);
            Assert.NotEqual(0, vm.Comission);
            Assert.False(vm.IsPosted);

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

            vm.Sales = new AccountViewModel()
            {
                profile = new ProfileViewModel()
                {
                    firstname = "a",
                    lastname = "a"
                },
                UserName = "a"
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Color = "a";
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Greige = new ProductViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Date = DateTimeOffset.UtcNow;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Material = new MaterialViewModel()
            {
                Id = 1
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

            

            vm.PreparationFabricWeight = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.RFDFabricWeight = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ActualPrice = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ConfirmPrice = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.CargoCost = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.InsuranceCost = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.FreightCost = 1;
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

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<FinishingPrintingCostCalculationMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationMachineMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationChemicalMapper>();
            });
            var mapper = configuration.CreateMapper();

            FinishingPrintingCostCalculationViewModel vm = new FinishingPrintingCostCalculationViewModel { Id = 1 };
            FinishingPrintingCostCalculationModel model = mapper.Map<FinishingPrintingCostCalculationModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
