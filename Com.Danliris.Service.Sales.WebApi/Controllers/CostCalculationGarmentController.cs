using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Helpers;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/cost-calculation-garments")]
	[Authorize]
	public class CostCalculationGarmentController : BaseController<CostCalculationGarment, CostCalculationGarmentViewModel, ICostCalculationGarment>
	{
		private readonly static string apiVersion = "1.0";
		private readonly IIdentityService Service;
		public CostCalculationGarmentController(IIdentityService identityService, IValidateService validateService, ICostCalculationGarment facade, IMapper mapper) : base(identityService, validateService, facade, mapper, apiVersion)
		{
			Service = identityService;
		}
		[HttpGet("pdf/{id}")]
		public async Task<IActionResult> GetPDF([FromRoute]int Id)
		{
			 

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

			try
			{
				var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
				int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
				CostCalculationGarment model = await Facade.ReadByIdAsync(Id);
				CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

				if (model == null)
				{
					Dictionary<string, object> Result =
						new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
						.Fail();
					return NotFound(Result);
				}
				else
				{
					CostCalculationGarmentPdfTemplate PdfTemplate = new CostCalculationGarmentPdfTemplate();
					MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);

					return new FileStreamResult(stream, "application/pdf")
					{
						FileDownloadName = "Cost Calculation Export Garment " + viewModel.RO_Number + ".pdf"
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

		[HttpGet("budget/{id}")]
		public async Task<IActionResult> GetBudget([FromRoute]int Id)
		{
			try
			{
				Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
				Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

				//await Service.GeneratePO(Id);
				CostCalculationGarment model = await Facade.ReadByIdAsync(Id);
				CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

				int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

				CostCalculationGarmentBudgetPdfTemplate PdfTemplate = new CostCalculationGarmentBudgetPdfTemplate();
				MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);

				return new FileStreamResult(stream, "application/pdf")
				{
					FileDownloadName = "Budget Export Garment " + viewModel.RO_Number + ".pdf"
				};
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