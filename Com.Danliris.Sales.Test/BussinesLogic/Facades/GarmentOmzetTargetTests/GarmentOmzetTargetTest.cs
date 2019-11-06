using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentOmzetTargetDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentOmzetTargetFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentOmzetTargetLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.GarmentOmzetTargetTest
{
    public class GarmentOmzetTargetTest : BaseFacadeTest<SalesDbContext, GarmentOmzetTargetFacade, GarmentOmzetTargetLogic, GarmentOmzetTarget, GarmentOmzetTargetDataUtil>
    {
        private const string ENTITY = "GarmentOmzetTargets";

        public GarmentOmzetTargetTest() : base(ENTITY)
        {
        }
    }
}