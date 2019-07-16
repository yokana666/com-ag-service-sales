using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class GarmentPreSalesContractControllerTest : BaseControllerTest<GarmentPreSalesContractController, GarmentPreSalesContract, GarmentPreSalesContractViewModel, IGarmentPreSalesContract>
    {
    }
}