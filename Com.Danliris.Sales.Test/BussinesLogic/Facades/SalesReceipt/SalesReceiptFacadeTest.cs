using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesReceipt;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.SalesReceipt
{
    public class SalesReceiptFacadeTest : BaseFacadeTest<SalesDbContext, SalesReceiptFacade, SalesReceiptLogic, SalesReceiptModel, SalesReceiptDataUtil>
    {
        private const string ENTITY = "SalesReceipt";
        public SalesReceiptFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var salesReceiptDetailLogic = new SalesReceiptDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesReceiptDetailLogic)))
                .Returns(salesReceiptDetailLogic);

            var salesReceiptLogic = new SalesReceiptLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesReceiptLogic)))
                .Returns(salesReceiptLogic);

            return serviceProviderMock;
        }
    }
}
