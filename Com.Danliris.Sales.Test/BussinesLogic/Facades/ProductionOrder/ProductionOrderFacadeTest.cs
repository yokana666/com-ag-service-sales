using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.ProductionOrder
{
    public class ProductionOrderFacadeTest : BaseFacadeTest<SalesDbContext, ProductionOrderFacade, ProductionOrderLogic, ProductionOrderModel, ProductionOrderDataUtil>
    {
        private const string ENTITY = "ProductionOrder";
        public ProductionOrderFacadeTest() : base(ENTITY)
        {
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);


            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = new ProductionOrderFacade(serviceProvider, dbContext);
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.ReadByIdAsync((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.DeleteAsync((int)all.Data.FirstOrDefault().Id);
            Assert.NotEqual(Response, 0);
        }



        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            var response = await facade.UpdateAsync((int)all.Data.FirstOrDefault().Id, data);

            Assert.NotEqual(response, 0);
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);
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
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailObject, serviceProviderMock.Object, identityService, dbContext);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            var productionOrderLogic = new ProductionOrderLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrderLogic)))
                .Returns(productionOrderLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void UpdateIsRequestedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
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
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
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
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedTrue((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedFalse((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateDistributedQuantity_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            List<double> distributedQuantity = new List<double>()
            {
                (int)all.Data.FirstOrDefault().DistributedQuantity
            };
            var response = await facade.UpdateRequestedFalse(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderQuantityByYearAndOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);

            var result = facade.GetMonthlyOrderQuantityByYearAndOrderType(data.DeliveryDate.Year, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderIdsByOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);

            var result = facade.GetMonthlyOrderIdsByOrderType(data.DeliveryDate.Year, data.DeliveryDate.Month, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCalculated((int)all.Data.FirstOrDefault().Id, true);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            await Assert.ThrowsAnyAsync<Exception>(() => facade.UpdateIsCalculated(0, true));

        }
    }
}
