using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.FinishingPrintingSalesContract
{
    public class ShinFinishingPrintingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, ShinFinishingPrintingSalesContractFacade, ShinFinishingPrintingSalesContractLogic, FinishingPrintingSalesContractModel, ShinFinisihingPrintingSalesContractDataUtil>
    {
        private const string ENTITY = "NewFinishingPrintingSalesContract";
        public ShinFinishingPrintingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var finishingprintingDetailLogic = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new ShinFinishingPrintingSalesContractLogic(finishingprintingDetailLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinFinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Create_Buyer_Type_Ekspor_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinFinishingPrintingSalesContractFacade facade = new ShinFinishingPrintingSalesContractFacade(serviceProvider, dbContext);
            
            var data = await DataUtil(facade, dbContext).GetNewData();
            data.BuyerType = "ekspor";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }
    }
}
