using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/finishing-printing-sales-contracts")]
    [Authorize]
    public class FinishingPrintingSalesContractController : BaseController<FinishingPrintingSalesContractModel, FinishingPrintingSalesContractViewModel, IFinishingPrintingSalesContract>
    {
        private readonly static string apiVersion = "1.0";
        private readonly IHttpClientService HttpClientService;
        public FinishingPrintingSalesContractController(IIdentityService identityService, IValidateService validateService, IFinishingPrintingSalesContract facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            HttpClientService = serviceProvider.GetService<IHttpClientService>();
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
                FinishingPrintingSalesContractModel model = await Facade.ReadByIdAsync(Id);

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


                    FinishingPrintingSalesContractViewModel viewModel = Mapper.Map<FinishingPrintingSalesContractViewModel>(model);

                    /* Get Buyer */
                    var response = HttpClientService.GetAsync($@"{APIEndpoint.Core}{BuyerUri}/" + viewModel.Buyer.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                    var json = result.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> buyer = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());

                    /* Get AccountBank */
                    var responseBank = HttpClientService.GetAsync($@"{APIEndpoint.Core}{BankUri}/" + viewModel.AccountBank.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultBank = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBank.Result);
                    var jsonBank = resultBank.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> bank = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonBank.ToString());

                    var currencyBankObj = new CurrencyViewModel();
                    object objResult = new object();
                    if (bank.TryGetValue("Currency", out objResult))
                    {
                        currencyBankObj = JsonConvert.DeserializeObject<CurrencyViewModel>(objResult.ToString());
                    }

                    /* Get Currencies */
                    //var responseCurrencies = httpClient.GetAsync($@"{APIEndpoint.Core}{CurrenciesUri}/" + viewModel.AccountBank.Currency.Id).Result.Content.ReadAsStringAsync();
                    //Dictionary<string, object> resultCurrencies = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseCurrencies.Result);
                    //var jsonCurrencies = resultCurrencies.Single(p => p.Key.Equals("data")).Value;
                    //Dictionary<string, object> currencies = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonCurrencies.ToString());


                    viewModel.Buyer.City = buyer.TryGetValue("City", out objResult) ? objResult.ToString() : "";
                    viewModel.Buyer.Address = buyer.TryGetValue("Address", out objResult) ? objResult.ToString() : "";
                    viewModel.Buyer.Contact = buyer.TryGetValue("Contact", out objResult) ? objResult.ToString() : "";
                    viewModel.Buyer.Country = buyer.TryGetValue("Country", out objResult) ? objResult.ToString() : "";

                    viewModel.AccountBank.BankAddress = bank.TryGetValue("BankAddress", out objResult) ? objResult.ToString() : "";
                    viewModel.AccountBank.SwiftCode = bank.TryGetValue("SwiftCode", out objResult) ? objResult.ToString() : "";

                    viewModel.AccountBank.Currency = new CurrencyViewModel();
                    viewModel.AccountBank.Currency.Description = currencyBankObj.Description;
                    viewModel.AccountBank.Currency.Symbol = currencyBankObj.Symbol;
                    viewModel.AccountBank.Currency.Rate = currencyBankObj.Rate;
                    viewModel.AccountBank.Currency.Code = currencyBankObj.Code;


                    if (viewModel.Buyer.Type != "Ekspor")
                    {
                        FinishingPrintingSalesContractPDFTemplate PdfTemplate = new FinishingPrintingSalesContractPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "finishing printing sales contract (id)" + viewModel.SalesContractNo + ".pdf"
                        };
                    }
                    else
                    {
                        FinishingPrintingSalesContractExportPDFTemplate PdfTemplate = new FinishingPrintingSalesContractExportPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "finishing printing sales contract (en) " + viewModel.SalesContractNo + ".pdf"
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
