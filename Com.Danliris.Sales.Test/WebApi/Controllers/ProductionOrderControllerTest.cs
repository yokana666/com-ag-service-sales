using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.ViewModels.Report.OrderStatusReport;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Com.Danliris.Service.Sales.WebApi.Controllers.ProductionOrderController;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class ProductionOrderControllerTest : BaseControllerTest<ProductionOrderController, ProductionOrderModel, ProductionOrderViewModel, IProductionOrder>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(ProductionOrderModel));
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

        private async Task<int> GetStatusCodePut((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IProductionOrder> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id, ProductionOrderViewModel viewModel)
        {
            ProductionOrderController controller = this.GetController(mocks);
            IActionResult response = await controller.Put(id, viewModel);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Put_IsRequested_True_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedTrue(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedTrue(ids);

            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_False_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedFalse(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedFalse(ids);
            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_True_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedTrue(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedTrue((int)viewModel.Id);
            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_False_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedFalse(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedFalse((int)viewModel.Id);
            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_Distributed_Quantity_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var distributedQty = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id,
                DistributedQuantity = distributedQty
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateDistributedQuantity(It.IsAny<List<int>>(),It.IsAny<List<double>>())).ReturnsAsync(1);
            List<SppParams> data = new List<SppParams>();
            List<int> ids = new List<int>();
            List<double> distributedQuantity = new List<double>();
            foreach (var item in data)
            {
                ids.Add(int.Parse(item.id));
                distributedQuantity.Add((double)item.distributedQuantity);
            };

            var controller = GetController(mocks);
            var response = await controller.PutDistributedQuantity(data);
            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public void Should_ReturnOK_GetMonthlySummaryByYearAndOrderType_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderQuantityByYearAndOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<YearlyOrderQuantity>());
            var controller = GetController(mocks);
            var response = controller.GetMonthlySummaryByYearAndOrderType(0, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_GetMonthlySummaryByYearAndOrderType_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderQuantityByYearAndOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.GetMonthlySummaryByYearAndOrderType(1, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void Should_ReturnOK_GetMonthlyOrderIdsByOrderTypeId_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderIdsByOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<MonthlyOrderQuantity>());
            var controller = GetController(mocks);
            var response = controller.GetMonthlyOrderIdsByOrderTypeId(0, 0, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_GetMonthlyOrderIdsByOrderTypeId_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderIdsByOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.GetMonthlyOrderIdsByOrderTypeId(1, 1, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(1, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(1, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_ID_0()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(0, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

    }
}
