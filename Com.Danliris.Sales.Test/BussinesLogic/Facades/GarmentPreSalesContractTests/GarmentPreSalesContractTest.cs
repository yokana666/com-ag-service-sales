using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentPreSalesContractTest
{
    public class GarmentPreSalesContractTest : BaseFacadeTest<SalesDbContext, GarmentPreSalesContractFacade, GarmentPreSalesContractLogic, GarmentPreSalesContract, GarmentPreSalesContractDataUtil>
    {
        private const string ENTITY = "GarmentPreSalesContracts";

        public GarmentPreSalesContractTest() : base(ENTITY)
        {
        }


    }
}