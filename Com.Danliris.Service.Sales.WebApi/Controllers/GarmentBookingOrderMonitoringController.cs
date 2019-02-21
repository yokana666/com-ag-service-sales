using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    //[Produces("application/json")]
    //[ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/sales/garment-booking-orders/monitoring")]
    //[Authorize]
    public class GarmentBookingOrderMonitoringController //: Controller
    {
        //private string ApiVersion = "1.0.0";
        //private readonly GarmentBookingOrderMonitoringFacade _facade;

        //public GarmentBookingOrderMonitoringController(GarmentBookingOrderMonitoringFacade facade)
        //{
        //    _facade = facade;
        //}

        //[HttpGet]
        //public IActionResult GetReportAll(string no, string buyerName, string sectionName, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        //{
        //    int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        //    string accept = Request.Headers["Accept"];

        //    try
        //    {

        //        var data = _facade.GetReport(no, buyerName, sectionName, dateFrom, dateTo, page, size, Order, offset);

        //        return Ok(new
        //        {
        //            apiVersion = ApiVersion,
        //            data = data.Item1,
        //            info = new { total = data.Item2 },
        //            message = General.OK_MESSAGE,
        //            statusCode = General.OK_STATUS_CODE
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        Dictionary<string, object> Result =
        //            new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
        //            .Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
        //    }
        //}
    }
}
