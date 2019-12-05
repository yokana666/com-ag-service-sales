using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
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
    [Route("v{version:apiVersion}/sales/finishing-printing-cost-calculations")]
    [Authorize]
    public class FinishingPrintingCostCalculationController : BaseController<FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationViewModel, IFinishingPrintingCostCalculationService>
    {
        private readonly static string apiVersion = "1.0";
        public FinishingPrintingCostCalculationController(IIdentityService identityService, IValidateService validateService, IFinishingPrintingCostCalculationService facade, IMapper mapper) : base(identityService, validateService, facade, mapper, apiVersion)
        {
        }

    }
}
