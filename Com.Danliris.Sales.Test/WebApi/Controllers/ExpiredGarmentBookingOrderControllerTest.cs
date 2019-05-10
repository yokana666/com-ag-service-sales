using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class ExpiredGarmentBookingOrderControllerTest : BaseControllerTest<ExpiredGarmentBookingOrderController, GarmentBookingOrder, GarmentBookingOrderViewModel, IExpiredGarmentBookingOrder>
    {
        //[Fact]
        //public void Get_WithoutException_ReturnOK_readExpired()
        //{
        //    var mocks = this.GetMocks();
        //    mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<GarmentBookingOrder>(new List<GarmentBookingOrder>(), 0, new Dictionary<string, string>(), new List<string>()));
        //    mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);

        //    int statusCode = this.GetStatusCodeGet(mocks);
        //    Assert.Equal((int)HttpStatusCode.OK, statusCode);
        //}
    }
}
