using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel.GarmentPurchaseRequestViewModel
{
    public class GarmentPurchaseRequestViewModel : BaseViewModel
    {
        public string PRNo { get; set; }
        public string RONo { get; set; }

        public BuyerViewModel Buyer { get; set; }

        public string Article { get; set; }

        public DateTimeOffset? Date { get; set; }
        public DateTimeOffset? ExpectedDeliveryDate { get; set; }
        public DateTimeOffset? ShipmentDate { get; set; }

        public UnitViewModel Unit { get; set; }

        public bool IsPosted { get; set; }
        public bool IsUsed { get; set; }
        public string Remark { get; set; }

        public List<GarmentPurchaseRequestItemViewModel> Items { get; set; }
    }
}
