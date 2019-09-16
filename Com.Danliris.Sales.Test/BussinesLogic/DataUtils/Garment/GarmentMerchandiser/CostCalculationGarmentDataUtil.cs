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
            data.Section = "A";
            data.Article = "test";
            data.ConfirmDate = DateTimeOffset.Now;
            data.DeliveryDate = DateTimeOffset.Now;
            data.BuyerId = "1";
            data.BuyerCode = "Test";
            data.BuyerName = "Text";
            data.BuyerBrandId = 1;
            data.BuyerBrandCode = "Test";
            data.BuyerBrandName = "Test";
            data.RO_Number = "Test";
            data.Description = "Test";
            data.ComodityCode = "Test";
            data.Quantity = 1;
            data.ConfirmPrice = 1;
            data.UOMUnit = "Test";
            data.SectionName = "FRANSISKA YULIANI";
            data.PreSCId = garmentPreSalesContractData.Id;
            data.PreSCNo = garmentPreSalesContractData.SCNo;
            data.UnitCode = "C2A";
            data.UnitName = "test";
            data.IsROAccepted = false;
            data.ROAcceptedBy = "test";
            data.ROAcceptedDate = DateTimeOffset.Now;
            data.IsROAvailable = false;
            data.ROAvailableBy = "test";
            data.ROAvailableDate = DateTimeOffset.Now;
            data.IsRODistributed = false;
            data.RODistributionBy = "test";
            data.RODistributionDate = DateTimeOffset.Now;
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
