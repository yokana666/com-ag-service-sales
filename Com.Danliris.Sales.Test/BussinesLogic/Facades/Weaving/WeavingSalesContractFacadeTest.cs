using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Weaving;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Weaving;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.Weaving
{
    public class WeavingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, WeavingSalesContractFacade, WeavingSalesContractLogic, WeavingSalesContractModel, WeavingSalesContractDataUtil>
    {
        private const string ENTITY = "SpinningSalesContract";
        public WeavingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);


            var spinningLogic = new WeavingSalesContractLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeavingSalesContractLogic)))
                .Returns(spinningLogic);

            return serviceProviderMock;
        }
    }
}
