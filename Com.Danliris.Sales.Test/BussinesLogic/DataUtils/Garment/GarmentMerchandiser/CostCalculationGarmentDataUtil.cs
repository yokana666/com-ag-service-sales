using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser
{
    public class CostCalculationGarmentDataUtil : BaseDataUtil<CostCalculationGarmentFacade, CostCalculationGarment>
    {
        public CostCalculationGarmentDataUtil(CostCalculationGarmentFacade facade) : base(facade)
        {
        }

        public override CostCalculationGarment GetNewData()
        {
            var data = base.GetNewData();

            data.CostCalculationGarment_Materials = new List<CostCalculationGarment_Material>();

            return data;
        }
    }
}
