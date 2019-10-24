using AutoMapper;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentOmzetTargetControllerTest : BaseControllerTest<GarmentOmzetTargetController, GarmentOmzetTarget, GarmentOmzetTargetViewModel, IGarmentOmzetTarget>
    {        
    }
}