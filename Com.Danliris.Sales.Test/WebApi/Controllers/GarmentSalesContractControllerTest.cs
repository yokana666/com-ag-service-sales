using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentSalesContractControllerTest : BaseControllerTest<GarmentSalesContractController, GarmentSalesContract, GarmentSalesContractViewModel, IGarmentSalesContract>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(GarmentSalesContract));
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
    }
}

