using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentOmzetTargetInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentOmzetTargetViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentOmzetTargetControllerTest : BaseControllerTest<GarmentOmzetTargetController, GarmentOmzetTarget, GarmentOmzetTargetViewModel, IGarmentOmzetTarget>
    {
        [Fact]
        public void Validate_Default()
        {
            GarmentOmzetTargetViewModel defaultViewModel = new GarmentOmzetTargetViewModel();

            var defaultValidationResult = defaultViewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

        [Fact]
        public void Validate_Filled()
        {
            var mock = GetMocks();

            mock.Facade.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ReadResponse<GarmentOmzetTarget>(new List<GarmentOmzetTarget>(), 10, new Dictionary<string, string>(), new List<string>()));

            mock.ServiceProvider.Setup(s => s.GetService(typeof(IGarmentOmzetTarget)))
                .Returns(mock.Facade.Object);

            GarmentOmzetTargetViewModel filledViewModel = new GarmentOmzetTargetViewModel
            {
                SectionName = "Name",
                SectionId = 1,
                SectionCode = "Code",
                YearOfPeriod = "Test",
                MonthOfPeriod = "Test",
                QuaterCode = "Test",
                Amount = 0
            };

            ValidationContext validationContext = new ValidationContext(filledViewModel, mock.ServiceProvider.Object, null);

            var filledValidationResult = filledViewModel.Validate(validationContext);
            Assert.True(filledValidationResult.Count() > 0);
        }
    }
}