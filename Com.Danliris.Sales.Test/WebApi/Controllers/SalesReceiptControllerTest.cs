using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesReceiptProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class SalesReceiptControllerTest : BaseControllerTest<SalesReceiptController, SalesReceiptModel, SalesReceiptViewModel, ISalesReceiptContract>
    {
        [Fact]
        public void Get_Sales_Receipt_PDF_Success()
        {
            var vm = new SalesReceiptViewModel()
            {

                SalesReceiptDetails = new List<SalesReceiptDetailViewModel>()
                {
                    new SalesReceiptDetailViewModel()
                    {
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesReceiptViewModel>(It.IsAny<SalesReceiptModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Sales_Receipt_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesReceiptModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Receipt_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<SalesReceiptMapper>();
                cfg.AddProfile<SalesReceiptDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            SalesReceiptViewModel salesReceiptViewModel = new SalesReceiptViewModel { Id = 1 };
            SalesReceiptModel salesReceiptModel = mapper.Map<SalesReceiptModel>(salesReceiptViewModel);

            Assert.Equal(salesReceiptViewModel.Id, salesReceiptModel.Id);

            SalesReceiptDetailViewModel salesReceiptDetailViewModel = new SalesReceiptDetailViewModel { Id = 1 };
            SalesReceiptDetailModel salesReceiptDetailModel = mapper.Map<SalesReceiptDetailModel>(salesReceiptDetailViewModel);

            Assert.Equal(salesReceiptDetailViewModel.Id, salesReceiptDetailModel.Id);
        }
    }
}
