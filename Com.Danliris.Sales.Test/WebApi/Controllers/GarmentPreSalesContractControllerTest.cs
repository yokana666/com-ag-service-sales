using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentPreSalesContractControllerTest : BaseControllerTest<GarmentPreSalesContractController, GarmentPreSalesContract, GarmentPreSalesContractViewModel, IGarmentPreSalesContract>
    {
        private async Task<int> GetStatusCodePatch((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentPreSalesContract> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id)
        {
            GarmentPreSalesContractController controller = GetController(mocks);

            JsonPatchDocument<GarmentPreSalesContract> patch = new JsonPatchDocument<GarmentPreSalesContract>();
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
        public async Task Patch_InvalidId_ReturnBadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            var id = 1;
            var viewModel = new GarmentPreSalesContractViewModel()
            {
                Id = id + 1
            };
            mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);

            int statusCode = await this.GetStatusCodePatch(mocks, id);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Patch_ValidId_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new GarmentPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ReturnsAsync(1);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            int statusCode = await this.GetStatusCodePatch(mocks, id);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Patch_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Throws(this.GetServiceValidationException());
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            int statusCode = await this.GetStatusCodePatch(mocks, 1);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Patch_ThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new GarmentPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePatch(mocks, id);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}