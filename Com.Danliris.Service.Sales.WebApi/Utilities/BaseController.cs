using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Utilities
{
    public abstract class BaseController<TModel, TViewModel, TFacade> : Controller
         where TModel : BaseModel
         where TFacade : IBaseFacade<TModel>
    {
        protected readonly IMapper Mapper;
        protected readonly IdentityService IdentityService;
        protected readonly ValidateService ValidateService;
        protected readonly TFacade Facade;
        protected readonly string ApiVersion;

        public BaseController(IMapper mapper, IdentityService identityService, ValidateService validateService, TFacade facade, string apiVersion)
        {
            this.Mapper = mapper;
            this.IdentityService = identityService;
            this.ValidateService = validateService;
            this.Facade = facade;
            this.ApiVersion = apiVersion;
        }

        private void ValidateUser()
        {
            IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
            IdentityService.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
        }

        private void ValidateViewModel(TViewModel viewModel)
        {
            ValidateService.Validate(viewModel);
        }

        [HttpGet]
        public IActionResult Get(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<TViewModel> DataVM = Mapper.Map<List<TViewModel>>(Data.Item1);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<TViewModel>(DataVM, page, size, Data.Item2, Data.Item1.Count, Data.Item3, Data.Item4);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ValidateUser();
                ValidateViewModel(viewModel);

                TModel model = Mapper.Map<TModel>(viewModel);
                await Facade.CreateAsync(model);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.CREATED_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok();
                return Created(String.Concat(Request.Path, "/", 0), Result);
            }
            catch (ServiceValidationException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TModel model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    TViewModel viewModel = Mapper.Map<TViewModel>(model);
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<TViewModel>(viewModel);
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

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ValidateUser();
                ValidateViewModel(viewModel);

                TModel model = Mapper.Map<TModel>(viewModel);

                if (id != model.Id)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }

                await Facade.UpdateAsync(id, model);

                return NoContent();
            }
            catch (ServiceValidationException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ValidateUser();
                await Facade.DeleteAsync(id);

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
