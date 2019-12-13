using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class SalesInvoiceControllerTest : BaseControllerTest<SalesInvoiceController, SalesInvoiceModel, SalesInvoiceViewModel, ISalesInvoiceContract>
    {
        [Fact]
        public void Get_PDF_Success()
        {
            var vm = new SalesInvoiceViewModel()
            {
                DeliveryOrderNo = "DeliveryOrderNo",
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                BuyerName = "BuyerName",
                BuyerNPWP = "BuyerNPWP",
                CurrencyCode = "CurrencyCode",
                Notes = "Notes",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        UnitName = "UnitName",
                        Amount = 1,
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceModel));
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
