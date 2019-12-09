using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IFinishingPrintingPreSalesContractFacade fpPreSalesContractFacade;
        public FinishingPrintingCostCalculationController(IIdentityService identityService, IValidateService validateService, IFinishingPrintingCostCalculationService facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            fpPreSalesContractFacade = serviceProvider.GetService<IFinishingPrintingPreSalesContractFacade>();
        }

        public override async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                FinishingPrintingCostCalculationModel model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    FinishingPrintingCostCalculationViewModel viewModel = Mapper.Map<FinishingPrintingCostCalculationViewModel>(model);
                    var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)viewModel.PreSalesContract.Id);
                    viewModel.Instruction.Steps = viewModel.Machines.Select(x => x.Step).ToList();
                    viewModel.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<FinishingPrintingCostCalculationViewModel>(viewModel);
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
    }
}
