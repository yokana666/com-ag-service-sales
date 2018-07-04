using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Spinning;
using Com.Danliris.Service.Sales.Lib.Models.Spinning;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
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
                    string BuyerUri = "master/buyers";
                    string BankUri = "master/account-banks";
                    string CurrenciesUri = "master/currencies";
                    string Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);


                    SpinningSalesContractViewModel viewModel = Mapper.Map<SpinningSalesContractViewModel>(model);



                    /* Get Buyer */
                    var response = httpClient.GetAsync($@"{APIEndpoint.Core}{BuyerUri}/" + viewModel.Buyer.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                    var json = result.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> buyer = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());

                    /* Get AccountBank */
                    var responseBank = httpClient.GetAsync($@"{APIEndpoint.Core}{BankUri}/" + viewModel.AccountBank.AccountCurrencyId).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultBank = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBank.Result);
                    var jsonBank = resultBank.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> bank = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonBank.ToString());

                    /* Get Currencies */
                    var responseCurrencies = httpClient.GetAsync($@"{APIEndpoint.Core}{CurrenciesUri}/" + viewModel.AccountBank.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultCurrencies = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseCurrencies.Result);
                    var jsonCurrencies = resultCurrencies.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> currencies = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonCurrencies.ToString());


                    viewModel.Buyer.City = buyer["City"] != null ? buyer["City"].ToString() : "";
                    viewModel.Buyer.Address = buyer["Address"] != null ? buyer["Address"].ToString() : "";
                    viewModel.Buyer.Contact = buyer["Contact"] != null ? buyer["Contact"].ToString() : "";
                    viewModel.Buyer.Country = buyer["Country"] != null ? buyer["Country"].ToString() : "";

                    viewModel.AccountBank.BankAddress = bank["BankAddress"].ToString();
                    viewModel.AccountBank.SwiftCode = bank["SwiftCode"].ToString();

                    viewModel.AccountBank.Currency = new CurrencyViewModel();
                    viewModel.AccountBank.Currency.Description = currencies["Description"] != null ? currencies["Description"].ToString() : "";
                    viewModel.AccountBank.Currency.Symbol = currencies["Symbol"].ToString();
                    viewModel.AccountBank.Currency.Rate = Convert.ToDouble(currencies["Rate"].ToString());
                    viewModel.AccountBank.Currency.Code = currencies["Code"].ToString();


                    if (viewModel.Buyer.Type != "Ekspor")
                    {
                        SpinningSalesContractModelPDFTemplate PdfTemplate = new SpinningSalesContractModelPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "spinning sales contract (id)" + viewModel.SalesContractNo + ".pdf"
                        };
                    }
                    else
                    {
                        SpinningSalesContractModelExportPDFTemplate PdfTemplate = new SpinningSalesContractModelExportPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "spinning sales contract (en) " + viewModel.SalesContractNo + ".pdf"
                        };
                    }
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
