using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Garment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class ROGarmentValidationFacadeTest
    {
        private const string ENTITY = "ROGarmentValidation";

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        protected SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected CostCalculationGarmentDataUtil DataUtil(CostCalculationGarmentFacade facade, IServiceProvider serviceProvider = null, SalesDbContext dbContext = null)
        {
            serviceProvider = serviceProvider ?? GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);

            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(facade, garmentPreSalesContractDataUtil);
            return costCalculationGarmentDataUtil;
        }

        protected Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            CostCalculationGarmentLogic costCalculationGarmentLogic = new CostCalculationGarmentLogic(costCalculationGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext);

            GarmentPreSalesContractLogic garmentPreSalesContractLogic = new GarmentPreSalesContractLogic(identityService, dbContext);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(costCalculationGarmentLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
                .Returns(garmentPreSalesContractLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            RO_Garment_ValidationLogic rOGarmentValidationLogic = new RO_Garment_ValidationLogic(serviceProviderMock.Object);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(RO_Garment_ValidationLogic)))
                .Returns(rOGarmentValidationLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void Validate_RO_Garment_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();

            RO_Garment_ValidationFacade garmentValidationFacade = new RO_Garment_ValidationFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            var result = await garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async void Validate_RO_Garment_Error()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.IsPRMaster = false;
            }

            RO_Garment_ValidationFacade garmentValidationFacade = new RO_Garment_ValidationFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }

        [Fact]
        public async void Validate_RO_Garment_Error_Category_PROCESS()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.CategoryName = "PROCESS";
                material.IsPRMaster = false;
            }

            RO_Garment_ValidationFacade garmentValidationFacade = new RO_Garment_ValidationFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }

        [Fact]
        public async void Validate_RO_Garment_Error_Category_Mixed()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.IsPRMaster = false;
            }
            dataCostCalculationGarment.CostCalculationGarment_Materials.Add(new CostCalculationGarment_Material
            {
                ProductId = "2",
                CategoryName = "PROCESS",
                IsPRMaster = false
            });

            RO_Garment_ValidationFacade garmentValidationFacade = new RO_Garment_ValidationFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }
    }
}
