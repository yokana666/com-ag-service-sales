using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentPreSalesContractTest
{
    public class GarmentPreSalesContractTest : BaseFacadeTest<SalesDbContext, GarmentPreSalesContractFacade, GarmentPreSalesContractLogic, GarmentPreSalesContract, GarmentPreSalesContractDataUtil>
    {
        private const string ENTITY = "GarmentPreSalesContracts";

        public GarmentPreSalesContractTest() : base(ENTITY)
        {
        }

        [Fact]
        public async void PreSalesPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.PreSalesPost(listData,"test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void PreSalesUnPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            var Response = await facade.PreSalesUnpost(data.Id, "test");
            Assert.NotEqual(Response, 0);
        }
    }
}