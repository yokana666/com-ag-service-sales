using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentBookingOrderFacadeTest
{
    public class ExpiredGarmentBookingOrderFacadeTest
    {
        private const string ENTITY = "ExpiredGarmentBookingOrder";
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }
        private SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(new GarmentBookingOrderLogic(new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext), identityService, dbContext));

            return serviceProviderMock;
        }

        protected virtual ExpiredGarmentBookingOrderDataUtil DataUtil(ExpiredGarmentBookingOrderFacade facade, SalesDbContext dbContext = null)
        {
            ExpiredGarmentBookingOrderDataUtil dataUtil = new ExpiredGarmentBookingOrderDataUtil(facade);
            return dataUtil;
        }

        [Fact]
        public async void Get_Success_ReadExpired()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);


            var Response = expiredGarmentBookingOrder.ReadExpired(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Update_Success_ReadExpired()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ExpiredGarmentBookingOrderFacade facade = Activator.CreateInstance(typeof(ExpiredGarmentBookingOrderFacade), serviceProvider, dbContext) as ExpiredGarmentBookingOrderFacade;

            var data = await DataUtil(facade).GetTestData();
            data.IsBlockingPlan = false;
            data.Remark = "test";
            var listData = new List<GarmentBookingOrder> { data };

            var response = facade.BOCancelExpired(listData, "");

            Assert.NotEqual(response, 0);
        }
    }
}
