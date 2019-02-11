using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentBookingOrderControllerTest : BaseControllerTest<GarmentBookingOrderController, GarmentBookingOrder, GarmentBookingOrderViewModel, IGarmentBookingOrder>
    {
        
        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerErrors()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
