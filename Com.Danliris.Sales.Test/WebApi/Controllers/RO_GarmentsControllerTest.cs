using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ROGarmentInterface;
using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentROViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class RO_GarmentsControllerTest : BaseControllerTest<RO_GarmentsControllerprivate, RO_Garment, RO_GarmentViewModel, IROGarment>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(RO_Garment));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public async Task PostRO_Success_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.PostRO(It.IsAny<List<long>>())).ReturnsAsync(1);

            var controller = GetController(mocks);
            var response = await controller.PostRO(It.IsAny<List<long>>());

            var statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task PostRO_NoChanges_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.PostRO(It.IsAny<List<long>>())).ReturnsAsync(0);

            var controller = GetController(mocks);
            var response = await controller.PostRO(It.IsAny<List<long>>());

            var statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task PostRO_Failed_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.PostRO(It.IsAny<List<long>>())).ThrowsAsync(new Exception(string.Empty));

            var controller = GetController(mocks);
            var response = await controller.PostRO(It.IsAny<List<long>>());

            var statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task UnpostRO_Success_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UnpostRO(It.IsAny<long>())).ReturnsAsync(1);

            var controller = GetController(mocks);
            var response = await controller.UnpostRO(It.IsAny<long>());

            var statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task UnpostRO_Failed_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UnpostRO(It.IsAny<long>())).ThrowsAsync(new Exception(string.Empty));

            var controller = GetController(mocks);
            var response = await controller.UnpostRO(It.IsAny<long>());

            var statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
