using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentBookingOrderMonitoringControllerTest //: BaseControllerTest<GarmentBookingOrderMonitoringController, GarmentBookingOrder, GarmentBookingOrderMonitoringViewModel, IGarmentBookingOrderMonitoringInterface>
    {

        //protected ServiceValidationException GetServiceValidationException()
        //{
        //    Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
        //    List<ValidationResult> validationResults = new List<ValidationResult>();
        //    System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(this.ViewModel, serviceProvider.Object, null);
        //    return new ServiceValidationException(validationContext, validationResults);
        //}

        //protected (Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentBookingOrderMonitoringInterface> Facade, Mock<IMapper> Mapper) GetMocks()
        //{
        //    return (IdentityService: new Mock<IIdentityService>(), ValidateService: new Mock<IValidateService>(), Facade: new Mock<GarmentBookingOrderMonitoringFacade>(), Mapper: new Mock<IMapper>());
        //}


        //protected GarmentBookingOrderController GetController((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<GarmentBookingOrderMonitoringFacade> Facade, Mock<IMapper> Mapper) mocks)
        //{
        //    var user = new Mock<ClaimsPrincipal>();
        //    var claims = new Claim[]
        //    {
        //        new Claim("username", "unittestusername")
        //    };
        //    user.Setup(u => u.Claims).Returns(claims);
        //    GarmentBookingOrderController controller = (GarmentBookingOrderController)Activator.CreateInstance(typeof(GarmentBookingOrderController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object);
        //    controller.ControllerContext = new ControllerContext()
        //    {
        //        HttpContext = new DefaultHttpContext()
        //        {
        //            User = user.Object
        //        }
        //    };
        //    controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
        //    controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
        //    return controller;
        //}

        //protected int GetStatusCode(IActionResult response)
        //{
        //    return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        //}

        //private int GetStatusCodeGet((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<GarmentBookingOrderMonitoringFacade> Facade, Mock<IMapper> Mapper) mocks)
        //{
        //    GarmentBookingOrderController controller = this.GetController(mocks);
        //    IActionResult response = controller.Get();

        //    return this.GetStatusCode(response);
        //}
    }
}
