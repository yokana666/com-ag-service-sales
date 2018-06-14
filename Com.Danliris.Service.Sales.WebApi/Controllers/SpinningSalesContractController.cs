using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Spinning;
using Com.Danliris.Service.Sales.Lib.Models.Spinning;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Spinning;
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
    [Route("v{version:apiVersion}/sales/spinning-sales-contracts")]
    [Authorize]
    public class SpinningSalesContractController : BaseController<SpinningSalesContractModel, SpinningSalesContractViewModel, SpinningSalesContractFacade>
    {
        private readonly static string apiVersion = "1.0";
        public SpinningSalesContractController(IMapper mapper, IdentityService identityService, ValidateService validateService, SpinningSalesContractFacade spinningSalesContractFacade) : base(mapper, identityService, validateService, spinningSalesContractFacade, apiVersion)
        {
        }
    }
}
