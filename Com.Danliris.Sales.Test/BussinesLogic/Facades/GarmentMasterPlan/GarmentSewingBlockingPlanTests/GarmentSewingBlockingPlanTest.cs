using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.GarmentSewingBlockingPlanDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanTests
{
    public class GarmentSewingBlockingPlanTest 
    {
        private const string ENTITY = "SewingBlockingPlan";

        private const string USERNAME = "Unit Test";
        private IServiceProvider ServiceProvider { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
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

        private GarmentSewingBlockingPlanDataUtil DataUtil(GarmentSewingBlockingPlanFacade facade, SalesDbContext dbContext)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var WeekserviceProvider = GetWeekServiceProviderMock(dbContext).Object;
            var BOserviceProvider = GetBOServiceProviderMock(dbContext).Object;

            var weeklyPlanFacade = new WeeklyPlanFacade(WeekserviceProvider, dbContext);
            var weeklyPlanDataUtil = new WeeklyPlanDataUtil(weeklyPlanFacade);

            var bookingOrderFacade = new GarmentBookingOrderFacade(BOserviceProvider, dbContext);
            var garmentBookingOrderDataUtil = new GarmentBookingOrderDataUtil(bookingOrderFacade);

            var garmentSewingBlockingPlanFacade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);
            var garmentPurchaseRequestDataUtil = new GarmentSewingBlockingPlanDataUtil(garmentSewingBlockingPlanFacade, weeklyPlanDataUtil, garmentBookingOrderDataUtil);

            

            return garmentPurchaseRequestDataUtil;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSewingBlockingPlanLogic)))
                .Returns(new GarmentSewingBlockingPlanLogic( identityService, dbContext) );

            return serviceProviderMock;
        }

        protected virtual Mock<IServiceProvider> GetWeekServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeeklyPlanLogic)))
                .Returns(new WeeklyPlanLogic( identityService, dbContext) );

            return serviceProviderMock;
        }

        protected virtual Mock<IServiceProvider> GetBOServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var garmentBookingOrderItemLogic = new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext);
            var garmentBookingOrderLogic = new GarmentBookingOrderLogic(garmentBookingOrderItemLogic, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(garmentBookingOrderLogic);

            return serviceProviderMock;
        }


        //protected virtual TDataUtil DataUtil(TFacade facade, TDbContext dbContext = null)
        //{
        //    TDataUtil dataUtil = Activator.CreateInstance(typeof(TDataUtil), facade) as TDataUtil;
        //    return dataUtil;
        //}

        [Fact]
        public virtual async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);

            var data = DataUtil(facade, dbContext).GetNewData();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(Response.Id, 0);
        }

        [Fact]
        public virtual async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.DeleteAsync((int)data.Id);
            Assert.NotEqual(Response, 0);
        }
    }
}
