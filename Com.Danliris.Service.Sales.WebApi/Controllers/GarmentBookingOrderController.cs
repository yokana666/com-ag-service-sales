using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/garment-booking-orders")]
    [Authorize]
    public class GarmentBookingOrderController : BaseController<GarmentBookingOrder, GarmentBookingOrderViewModel, IGarmentBookingOrder>
    {
        private readonly static string apiVersion = "1.0";
        private readonly IIdentityService Service;
        private readonly IMapper Mapper;
        public GarmentBookingOrderController(IIdentityService identityService, IValidateService validateService, IGarmentBookingOrder facade, IMapper mapper) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            Service = identityService;
            this.Mapper = mapper;
        }
    }
}
