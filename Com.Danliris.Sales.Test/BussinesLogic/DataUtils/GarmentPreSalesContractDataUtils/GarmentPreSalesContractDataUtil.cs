using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils
{
    public class GarmentPreSalesContractDataUtil : BaseDataUtil<GarmentPreSalesContractFacade, GarmentPreSalesContract>
    {
        public GarmentPreSalesContractDataUtil(GarmentPreSalesContractFacade facade) : base(facade)
        {
        }

        public override GarmentPreSalesContract GetNewData()
        {
            return new GarmentPreSalesContract()
            {
                SCNo = "",
                SCDate = new DateTimeOffset(),
                SCType = "SAMPLE",
                SectionId = 1,
                SectionCode = "Test",
                BuyerAgentId = 1,
                BuyerAgentCode = "Test",
                BuyerAgentName = "Test",
                BuyerBrandId = 1,
                BuyerBrandCode = "Test",
                BuyerBrandName = "Test",
                OrderQuantity = 1,
                Remark = "Test",
                IsCC = false,
                IsPR = false
            };
        }

    }
}