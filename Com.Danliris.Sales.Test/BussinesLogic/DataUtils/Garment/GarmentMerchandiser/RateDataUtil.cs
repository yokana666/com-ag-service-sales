using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Danliris.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser
{
    public class RateDataUtil : BaseDataUtil<RateFacade, Rate>
    {
        public RateDataUtil(RateFacade facade) : base(facade)
        {
        }

        public override Rate GetNewData()
        {
            return new Rate
            {
                Name = ""
            };
        }
    }
}
