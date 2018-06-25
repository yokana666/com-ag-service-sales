using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Spinning;
using Com.Danliris.Service.Sales.Lib.Models.Spinning;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Spinning;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SpinningSalesContractModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {

                    SpinningSalesContractViewModel viewModel = Mapper.Map<SpinningSalesContractViewModel>(model);

                    SpinningSalesContractModelPDFTemplate PdfTemplate = new SpinningSalesContractModelPDFTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);

                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = $"sales contract.pdf"
                    };
                }
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
