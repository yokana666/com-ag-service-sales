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
using System.Linq;
using Newtonsoft.Json;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingCostCalculationFacade, FinishingPrintingCostCalculationLogic, FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationDataUtils>
    {
        private const string ENTITY = "FinishingPrintingCostCalculation";

        public FinishingPrintingCostCalculationFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public async void Update_Chemical_Success()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FinishingPrintingCostCalculationMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationMachineMapper>();
                cfg.AddProfile<FinishingPrintingCostCalculationChemicalMapper>();
            });
            var mapper = configuration.CreateMapper();

            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            var ch = new FinishingPrintingCostCalculationChemicalModel()
            {
                ChemicalId = 1,
                ChemicalQuantity = 1,
                ChemicalPrice = 1,
                ChemicalCurrency = "rp",
                ChemicalName = "name",
                ChemicalUom = "m"

            };
            data.Machines.FirstOrDefault().Chemicals.Add(ch);
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);

            //var vmData = mapper.Map<FinishingPrintingCostCalculationViewModel>(data);
            //vmData.Machines.FirstOrDefault().Chemicals.Clear();

            //var model = mapper.Map<FinishingPrintingCostCalculationModel>(vmData);
            //response = await facade.UpdateAsync((int)model.Id, model);
            //Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateMachine_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingCostCalculationFacade facade = Activator.CreateInstance(typeof(FinishingPrintingCostCalculationFacade), serviceProvider, dbContext) as FinishingPrintingCostCalculationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            data.Machines.Add(new FinishingPrintingCostCalculationMachineModel()
            {
                Chemicals = new List<FinishingPrintingCostCalculationChemicalModel>()
                        {
                            new FinishingPrintingCostCalculationChemicalModel()
                            {
                                ChemicalId = 1,
                                ChemicalQuantity = 1,
                                ChemicalPrice = 1,
                                ChemicalCurrency = "rp",
                                ChemicalName = "name",
                                ChemicalUom = "m"

                            }
                        },
                MachineElectric = 1,
                MachineLPG = 1,
                MachineName = "name",
                MachineProcess = "process",
                MachineSolar = 1,
                MachineSteam = 1,
                MachineWater = 1,
                StepId = 1,
                StepProcess = "prec",
                StepProcessArea = "area",
                Depretiation = 1,
                MachineId = 1
            });
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
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
                IsPosted = false,
                ManufacturingServiceCost = 1,
                HelperMaterial = 1,
                MiscMaterial = 1,
                Lubricant = 1,
                SparePart = 1,
                StructureMaintenance = 1,
                MachineMaintenance = 1,
                Embalase = 1,
                GeneralAdministrationCost = 1,
                DirectorOfficeCost = 1,
                BankMiscCost = 1

            };
            var response = vm.Validate(null);
            Assert.NotEmpty(response);
            Assert.NotNull(vm.Remark);
            Assert.NotNull(vm.ProductionOrderNo);
            Assert.False(vm.IsPosted);

            vm.PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = 1,
                Unit = new UnitViewModel()
                {
                    Name = "Printing"
                }
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

            vm.ScreenCost = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ScreenDocumentNo = "as";
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
            var configuration = new MapperConfiguration(cfg =>
            {
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
