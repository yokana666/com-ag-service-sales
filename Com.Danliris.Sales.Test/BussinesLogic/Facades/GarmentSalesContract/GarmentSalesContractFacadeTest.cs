using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentSalesContractDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentSalesContract
{
    public class GarmentSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, GarmentSalesContractFacade, GarmentSalesContractLogic, Service.Sales.Lib.Models.GarmentSalesContractModel.GarmentSalesContract, GarmentSalesContractDataUtil>
    {
        private const string ENTITY = "SpinningSalesContract";
        public GarmentSalesContractFacadeTest() : base(ENTITY)
        {

        }

        protected override GarmentSalesContractDataUtil DataUtil(GarmentSalesContractFacade facade, SalesDbContext dbContext = null)
        {
            var serviceProvider = GetServiceProviderMock(dbContext);
            CostCalculationGarmentFacade ccFacade = new CostCalculationGarmentFacade(serviceProvider.Object, dbContext);
            GarmentPreSalesContractFacade gpsCFacade = new GarmentPreSalesContractFacade(serviceProvider.Object, dbContext);
            GarmentPreSalesContractDataUtil gpCDataUtil = new GarmentPreSalesContractDataUtil(gpsCFacade);
            CostCalculationGarmentDataUtil ccDataUtil = new CostCalculationGarmentDataUtil(ccFacade, gpCDataUtil);
            GarmentSalesContractDataUtil dataUtil = Activator.CreateInstance(typeof(GarmentSalesContractDataUtil), facade, ccDataUtil) as GarmentSalesContractDataUtil;
            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            azureImageFacadeMock
                .Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            var ccGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            var ccGarmentLogic = new CostCalculationGarmentLogic(ccGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(ccGarmentLogic);

            var ccGarmentFacade = new CostCalculationGarmentFacade(serviceProviderMock.Object, dbContext);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(ICostCalculationGarment)))
                .Returns(ccGarmentFacade);

            GarmentPreSalesContractLogic gpscLogic = new GarmentPreSalesContractLogic(identityService, dbContext);
            serviceProviderMock
               .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
               .Returns(gpscLogic);

            var garmentSCItem = new GarmentSalesContractItemLogic(serviceProviderMock.Object, identityService, dbContext);

            var spinningLogic = new GarmentSalesContractLogic(garmentSCItem, serviceProviderMock.Object, identityService, dbContext);
            
            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSalesContractLogic)))
                .Returns(spinningLogic);

            

            return serviceProviderMock;
        }
    }
}
