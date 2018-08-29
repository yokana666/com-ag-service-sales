using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Danliris.Service.Sales.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/reports/weaving-sales-contract-report")]
    [Authorize]
    public class WeavingSalesContractReportController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly WeavingSalesContractFacade weavingSalesContractFacade;

        public WeavingSalesContractReportController(WeavingSalesContractFacade weavingSalesContractFacade)
        {
            this.weavingSalesContractFacade = weavingSalesContractFacade;
        }
    }
}
