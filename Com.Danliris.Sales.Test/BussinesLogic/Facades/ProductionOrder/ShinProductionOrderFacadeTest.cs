using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Report;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.ProductionOrder
{
    public class ShinProductionOrderFacadeTest : BaseFacadeTest<SalesDbContext, ShinProductionOrderFacade, ShinProductionOrderLogic, ProductionOrderModel, ShinProductionOrderDataUtil>
    {
        private const string ENTITY = "ShinProductionOrder";
        public ShinProductionOrderFacadeTest() : base(ENTITY)
        {
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();


            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.ReadByIdAsync((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetNewData();
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.DeleteAsync((int)all.Data.FirstOrDefault().Id);
            Assert.NotEqual(Response, 0);
        }



        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            var response = await facade.UpdateAsync((int)all.Data.FirstOrDefault().Id, all.Data.FirstOrDefault());

            Assert.NotEqual(response, 0);
        }

        protected override ShinProductionOrderDataUtil DataUtil(ShinProductionOrderFacade facade, SalesDbContext dbContext = null)
        {
            FinishingPrintingCostCalculationFacade ccFacade = new FinishingPrintingCostCalculationFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ShinFinishingPrintingSalesContractFacade scFacade = new ShinFinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ShinProductionOrderDataUtil dataUtil = new ShinProductionOrderDataUtil(facade, scFacade, ccFacade);
            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);

            var httpClientService = new Mock<IHttpClientService>();
            DailyAPiResult dailyAPiResult = new DailyAPiResult
            {
                data = new List<DailyOperationViewModel> {
                    new DailyOperationViewModel {
                        area = "Test",
                        color = "Color Test",
                        machine = "Machine Test",
                        orderNo = "a",
                        orderQuantity = 1,
                        step = "Test"
                    }
                }
            };

            FabricAPiResult fabricAPiResult = new FabricAPiResult
            {
                data = new List<FabricQualityControlViewModel> {
                    new FabricQualityControlViewModel {
                        grade = "Test",
                        orderNo = "a",
                        orderQuantity = 1
                    }
                }
            };

            httpClientService.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains("finishing-printing/daily-operations/production-order-report"))))
                .ReturnsAsync(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(dailyAPiResult)) });
            httpClientService.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains("finishing-printing/quality-control/defect"))))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(fabricAPiResult)) });


            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = "Username", TimezoneOffset = 7 });
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientService.Object);

            var ccLogic = new FinishingPrintingCostCalculationLogic(identityService, dbContext);
            serviceProviderMock.Setup(s => s.GetService(typeof(FinishingPrintingCostCalculationLogic)))
                .Returns(ccLogic);

            var productionOrderDetailLogic = new ProductionOrder_DetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var productionOrderlsLogic = new ProductionOrder_LampStandardLogic(serviceProviderMock.Object, identityService, dbContext);
            var productionOrderrwLogic = new ProductionOrder_RunWidthLogic(serviceProviderMock.Object, identityService, dbContext);

            var poDetailMock = new Mock<ProductionOrder_DetailLogic>();
            var poRWk = new Mock<ProductionOrder_RunWidthLogic>();
            var poLSMock = new Mock<ProductionOrder_LampStandardLogic>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_DetailLogic)))
                .Returns(productionOrderDetailLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_LampStandardLogic)))
                .Returns(productionOrderlsLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_RunWidthLogic)))
                .Returns(productionOrderrwLogic);


            var finishingprintingDetailObject = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new ShinFinishingPrintingSalesContractLogic(finishingprintingDetailObject, serviceProviderMock.Object, identityService, dbContext);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinFinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            var productionOrderLogic = new ShinProductionOrderLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinProductionOrderLogic)))
                .Returns(productionOrderLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void UpdateIsRequestedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "requestedTrue");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            var response = await facade.UpdateRequestedTrue(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsRequestedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "requestedFalse");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            var response = await facade.UpdateRequestedFalse(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedTrue(Convert.ToInt32(all.Data.FirstOrDefault().Id));

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedFalse(Convert.ToInt32(all.Data.FirstOrDefault().Id));

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateDistributedQuantity_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            List<double> distributedQuantity = new List<double>()
            {
                (int)all.Data.FirstOrDefault().DistributedQuantity
            };
            var response = await facade.UpdateDistributedQuantity(ids, distributedQuantity);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderQuantityByYearAndOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var result = facade.GetMonthlyOrderQuantityByYearAndOrderType(data.DeliveryDate.Year, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderIdsByOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var result = facade.GetMonthlyOrderIdsByOrderType(data.DeliveryDate.Year, data.DeliveryDate.Month, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCalculated(Convert.ToInt32(all.Data.FirstOrDefault().Id), true);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await Assert.ThrowsAnyAsync<Exception>(() => facade.UpdateIsCalculated(0, true));

        }
        [Fact]
        public async void Should_Success_Get_ProductionReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);
            
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();


            var tuple = await facade.GetReport(data.SalesContractNo, null, null, null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(3), 1, 25, "{}", 7);
            Assert.NotNull(tuple.Item1);


        }
        [Fact]
        public async void Shoould_Success_Get_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);
            
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var tuple = await facade.GenerateExcel(data.SalesContractNo, null, null, null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(3), 7);
            Assert.IsType<System.IO.MemoryStream>(tuple);


        }
    }
}
