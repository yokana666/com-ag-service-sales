using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/finishing-printing-sales-contracts")]
    [Authorize]
    public class FinishingPrintingSalesContractController : BaseController<FinishingPrintingSalesContractModel, FinishingPrintingSalesContractViewModel, FinishingPrintingSalesContractFacade>
    {
        private readonly static string apiVersion = "1.0";
        public FinishingPrintingSalesContractController(IMapper mapper, IdentityService identityService, ValidateService validateService, FinishingPrintingSalesContractFacade facade) : base(mapper, identityService, validateService, facade, apiVersion)
        {
        }
    }
}
