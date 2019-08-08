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
using Microsoft.AspNetCore.JsonPatch;
using Com.Danliris.Service.Sales.Lib.Utilities;

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
		public CostCalculationGarmentController(IIdentityService identityService, IValidateService validateService, ICostCalculationGarment facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
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

        [HttpGet("ro-garment-validation/{Id}")]
        public async Task<IActionResult> GetById_RO_Garment_Validation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                CostCalculationGarment model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

                    var productIds = viewModel.CostCalculationGarment_Materials.Select(m => m.Product.Id).Distinct().ToList();
                    var productDicts = await Facade.GetProductNames(productIds);

                    foreach (var material in viewModel.CostCalculationGarment_Materials)
                    {
                        material.Product.Name = productDicts.GetValueOrDefault(material.Product.Id);
                    }

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<CostCalculationGarmentViewModel>(viewModel);
                    return Ok(Result);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute]int id, [FromBody]JsonPatchDocument<CostCalculationGarment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = await Facade.ReadByIdAsync(id);
                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    patch.ApplyTo(model);

                    IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                    IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");

                    await Facade.UpdateAsync(id, model);

                    return NoContent();
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

        [HttpPut("isvalidate-ro-sample/{Id}")]
        public async Task<IActionResult> PutRoSample([FromRoute] int id, [FromBody] CostCalculationGarmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }

                model.IsValidatedROSample = true;
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");

                await Facade.UpdateAsync(id, model);

                return NoContent();
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