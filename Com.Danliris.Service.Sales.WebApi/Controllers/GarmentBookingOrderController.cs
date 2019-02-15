using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Danliris.Service.Sales.WebApi.Helpers;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/garment-booking-orders")]
    [Authorize]
    public class GarmentBookingOrderController : BaseController<GarmentBookingOrder, GarmentBookingOrderViewModel, IGarmentBookingOrder>
    {
        private readonly static string apiVersion = "1.0";
        private readonly IGarmentBookingOrder facades;
        private readonly IIdentityService Service;
        public GarmentBookingOrderController(IIdentityService identityService, IValidateService validateService, IGarmentBookingOrder facade, IMapper mapper) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            facades = facade;
            Service = identityService;
        }

        [HttpPut("BOCancel/{id}")]
        public async Task<IActionResult> CancelLeftOvers([FromRoute]int id, [FromBody] GarmentBookingOrderViewModel viewModel)
        {
            IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

            GarmentBookingOrder model = Mapper.Map<GarmentBookingOrder>(viewModel);
            try
            {
                await facades.BOCancel(id, model);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpPut("BODelete/{id}")]
        public async Task<IActionResult> DeleteLeftOvers([FromRoute]int id, [FromBody] GarmentBookingOrderViewModel viewModel)
        {
            IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

            GarmentBookingOrder model = Mapper.Map<GarmentBookingOrder>(viewModel);
            try
            {
                await facades.BODelete(id, model);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }
    }
}
