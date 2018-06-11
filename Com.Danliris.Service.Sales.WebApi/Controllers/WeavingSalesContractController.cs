using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Weaving;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/weaving-sales-contracts")]
    [Authorize]
    public class WeavingSalesContractController : BaseController<WeavingSalesContractModel, WeavingSalesContractViewModel, WeavingSalesContractFacade>
    {
        private readonly static string apiVersion = "1.0";
        public WeavingSalesContractController(IMapper mapper, IdentityService identityService, ValidateService validateService, WeavingSalesContractFacade weavingSalesContractFacade) : base(mapper, identityService, validateService, weavingSalesContractFacade, apiVersion)
        {
        }
    }
}
