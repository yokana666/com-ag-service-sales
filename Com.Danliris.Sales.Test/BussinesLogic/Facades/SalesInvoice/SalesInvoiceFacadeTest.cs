using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoice;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.SalesInvoice
{
    class SalesInvoiceFacadeTest : BaseFacadeTest<SalesDbContext, SalesInvoiceFacade, SalesInvoiceLogic, SalesInvoiceModel, SalesInvoiceDataUtil>
    {
        private const string ENTITY = "ProductionOrder";
        public SalesInvoiceFacadeTest() : base(ENTITY)
        {
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;
            var data = await DataUtil(facade).GetNewData();
            var model = await facade.CreateAsync(data);


            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = new SalesInvoiceFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade).GetNewData();
            await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.ReadByIdAsync((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;
            var data = await DataUtil(facade).GetNewData();
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;
            var data = await DataUtil(facade).GetNewData();
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.DeleteAsync((int)all.Data.FirstOrDefault().Id);
            Assert.NotEqual(Response, 0);
        }



        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;
            var data = await DataUtil(facade).GetNewData();
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
            var productionOrderDetailLogic = new SalesInvoiceDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            var siDetailMock = new Mock<SalesInvoiceDetailLogic>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceDetailLogic)))
                .Returns(productionOrderDetailLogic);

            var productionOrderLogic = new SalesInvoiceLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceLogic)))
                .Returns(productionOrderLogic);

            return serviceProviderMock;
        }
    }
}
