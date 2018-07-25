using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/production-orders")]
    [Authorize]
    public class ProductionOrderController : BaseController<ProductionOrderModel, ProductionOrderViewModel, IProductionOrder>
    {
        private readonly static string apiVersion = "1.0";
        public ProductionOrderController(IIdentityService identityService, IValidateService validateService, IProductionOrder productionOrderFacade, IMapper mapper) : base(identityService, validateService, productionOrderFacade, mapper, apiVersion)
        {
        }

    }
}
