using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Weaving;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                WeavingSalesContractModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {

                    string BuyerUri = "master/buyers";
                    string BankUri = "master/account-banks";
                    string Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                    WeavingSalesContractViewModel viewModel = Mapper.Map<WeavingSalesContractViewModel>(model);

                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    /* Get Buyer */
                    var response = httpClient.GetAsync($@"{APIEndpoint.Core}{BuyerUri}/" + viewModel.Buyer.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                    var json = result.Single(p => p.Key.Equals("data")).Value;       
                    Dictionary<string, object> buyer = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());

                    /* Get AccountBank */
                    var responseBank = httpClient.GetAsync($@"{APIEndpoint.Core}{BankUri}/" + viewModel.AccountBank.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultBank = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBank.Result);
                    var jsonBank = resultBank.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> bank = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonBank.ToString());


                    viewModel.Buyer.City = buyer["City"].ToString();
                    viewModel.Buyer.Address = buyer["Address"].ToString();
               
                    viewModel.AccountBank.BankAddress = bank["BankAddress"].ToString();

                    WeavingSalesContractModelPDFTemplate PdfTemplate = new WeavingSalesContractModelPDFTemplate();
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
