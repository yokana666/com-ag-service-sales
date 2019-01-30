using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.BookingOrder;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrder;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentBookingOrderControllerTest : BaseControllerTest<GarmentBookingOrderController, GarmentBookingOrder, GarmentBookingOrderViewModel, IGarmentBookingOrder>
    {
    }
}
