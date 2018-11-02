using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class CostCalculationGarmentControllerTest : BaseControllerTest<CostCalculationGarmentController, CostCalculationGarment, CostCalculationGarmentViewModel, ICostCalculationGarment>
    {
        [Fact]
        public async Task GetById_RO_Garment_Validation_NotNullModel_ReturnOK()
        {
            var ViewModel = this.ViewModel;
            ViewModel.CostCalculationGarment_Materials = new List<CostCalculationGarment_MaterialViewModel>();

            var mocks = GetMocks();
            mocks.Facade
                .Setup(f => f.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            mocks.Facade
                .Setup(f => f.GetProductNames(It.IsAny<List<long>>()))
                .ReturnsAsync(new Dictionary<long, string>());
            mocks.Mapper
                .Setup(m => m.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>()))
                .Returns(ViewModel);

            var controller = GetController(mocks);
            var response = await controller.GetById_RO_Garment_Validation(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_RO_Garment_Validation_NullModel_ReturnNotFound()
        {
            var mocks = GetMocks();
            mocks.Mapper.Setup(f => f.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>())).Returns(ViewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync((CostCalculationGarment)null);

            var controller = GetController(mocks);
            var response = await controller.GetById_RO_Garment_Validation(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_RO_Garment_Validation_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetById_RO_Garment_Validation(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
