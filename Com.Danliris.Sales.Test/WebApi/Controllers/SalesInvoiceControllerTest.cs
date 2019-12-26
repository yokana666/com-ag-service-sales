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
        public void Get_Delivery_Order_PDF_Success()
        {
            var vm = new SalesInvoiceViewModel()
            {
                DeliveryOrderNo = "DeliveryOrderNo",
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                Remark = "Remark",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        UnitName = "UnitName",
                        UomUnit = "PACKS",
                        Quantity = "Quantity",
                        Total = 1,
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Delivery_Order_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceModel));
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Delivery_Order_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }


        [Fact]
        public void Get_Sales_Invoice_PDF_Success()
        {
            var vm = new SalesInvoiceViewModel()
            {
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                DueDate  = DateTimeOffset.Now,
                DebtorIndexNo = "DebtorIndexNo",
                IDNo = "IDNo",
                CurrencySymbol = "Rp",
                Remark = "Remark",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        UnitCode = "UnitCode",
                        Quantity = "Quantity",
                        UomUnit = "PACKS",
                        UnitName = "UnitName",
                        Total = 1,
                        UnitPrice = 1,
                        Amount = 1,
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Sales_Invoice_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }
    }
}
