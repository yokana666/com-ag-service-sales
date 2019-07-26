using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser
{
    public class CostCalculationGarmentDataUtil : BaseDataUtil<CostCalculationGarmentFacade, CostCalculationGarment>
    {
        private readonly GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil;

        public CostCalculationGarmentDataUtil(CostCalculationGarmentFacade facade, GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil) : base(facade)
        {
            this.garmentPreSalesContractDataUtil = garmentPreSalesContractDataUtil;
        }

        public override async Task<CostCalculationGarment> GetNewData()
        {
            var garmentPreSalesContractData = await garmentPreSalesContractDataUtil.GetTestData();

            var data = await base.GetNewData();
            data.PreSCId = garmentPreSalesContractData.Id;
            data.PreSCNo = garmentPreSalesContractData.SCNo;
            data.UnitCode = "C2A";
            data.CostCalculationGarment_Materials = new List<CostCalculationGarment_Material>()
            {
                new CostCalculationGarment_Material
                {
                    ProductId = "1",
                    CategoryName = "FABRIC"
                }
            };

            return data;
        }
    }
}
