using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllerTests
{
    public class WeeklyPlanControllerTest : BaseControllerTest<WeeklyPlanController, WeeklyPlan, WeeklyPlanViewModel, IWeeklyPlanFacade>
    {
        protected override WeeklyPlanViewModel ViewModel
        {
            get
            {
                var viewModel = base.ViewModel;
                viewModel.Items = new List<WeeklyPlanItemViewModel>
                {
                    new WeeklyPlanItemViewModel()
                };
                return viewModel;
            }
        }

        [Fact]
        public void Get_Years_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetYears(It.IsAny<string>()))
                .Returns(new List<string> { });

            var controller = GetController(mocks);
            var response = controller.GetYears();

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_Years_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetYears(It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetYears();

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
