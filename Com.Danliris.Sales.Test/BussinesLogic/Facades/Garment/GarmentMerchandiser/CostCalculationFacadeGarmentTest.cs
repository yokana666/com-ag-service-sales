using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class CostCalculationGarmentFacadeTest : BaseFacadeTest<SalesDbContext, CostCalculationGarmentFacade, CostCalculationGarmentLogic, CostCalculationGarment, CostCalculationGarmentDataUtil>
    {
        private const string ENTITY = "CostCalculationGarment";

        public CostCalculationGarmentFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            CostCalculationGarmentLogic costCalculationGarmentLogic = new CostCalculationGarmentLogic(costCalculationGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(costCalculationGarmentLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }
    }
}
