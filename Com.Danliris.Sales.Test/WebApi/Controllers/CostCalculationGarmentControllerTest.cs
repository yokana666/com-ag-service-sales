using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void Validate_ViewModel()
        {
            List<CostCalculationGarmentViewModel> viewModels = new List<CostCalculationGarmentViewModel>
            {
                new CostCalculationGarmentViewModel()
                {
                    FabricAllowance = 0,
                    AccessoriesAllowance = 0
                },
                new CostCalculationGarmentViewModel()
                {
                    Quantity = 0,
                    DeliveryDate = DateTimeOffset.Now.AddDays(-1),
                    SMV_Cutting = 0,
                    SMV_Sewing = 0,
                    SMV_Finishing = 0,
                    ConfirmPrice = 0,
                    CostCalculationGarment_Materials = new List<CostCalculationGarment_MaterialViewModel>
                    {
                        new CostCalculationGarment_MaterialViewModel(),
                        new CostCalculationGarment_MaterialViewModel
                        {
                            Category = new CategoryViewModel { code = "CategoryCode" }
                        },
                        new CostCalculationGarment_MaterialViewModel
                        {
                            Category = new CategoryViewModel { code = "CategoryCode" },
                            Quantity = 0,
                            Conversion = 0
                        },
                        new CostCalculationGarment_MaterialViewModel
                        {
                            Category = new CategoryViewModel { code = "CategoryCode" },
                            UOMPrice = new UOMViewModel() { Unit = "Unit" },
                            UOMQuantity = new UOMViewModel() { Unit = "Unit" },
                            Conversion = 2
                        }
                    }
                }
            };

            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        private async Task<int> GetStatusCodePatch((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<ICostCalculationGarment> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id)
        {
            CostCalculationGarmentController controller = GetController(mocks);

            JsonPatchDocument<CostCalculationGarment> patch = new JsonPatchDocument<CostCalculationGarment>();
            IActionResult response = await controller.Patch(id, patch);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Patch_InvalidId_ReturnNotFound()
        {
            int statusCode = await this.GetStatusCodePatch(GetMocks(), 1);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public async Task Patch_ValidId_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<CostCalculationGarmentViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new CostCalculationGarmentViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<CostCalculationGarment>())).ReturnsAsync(1);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            int statusCode = await this.GetStatusCodePatch(mocks, id);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Patch_ThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<CostCalculationGarmentViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new CostCalculationGarmentViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<CostCalculationGarment>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePatch(mocks, id);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Update_Ro_Sample()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<CostCalculationGarmentViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new CostCalculationGarmentViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<CostCalculationGarment>())).ReturnsAsync(1);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            var controller = GetController(mocks);
            var response = await controller.PutRoSample(id, It.IsAny<CostCalculationGarmentViewModel>());

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Update_Ro_Sample_ThrowException()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<CostCalculationGarmentViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new CostCalculationGarmentViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<CostCalculationGarmentViewModel>(It.IsAny<CostCalculationGarment>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<CostCalculationGarment>())).ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.PutRoSample(id, It.IsAny<CostCalculationGarmentViewModel>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
