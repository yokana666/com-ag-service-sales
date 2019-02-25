using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels
{
    public class GarmentSewingBlockingPlanViewModel : BaseViewModel, IValidatableObject
    {
        public long BookingOrderId { get; set; }
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }

        public List<GarmentSewingBlockingPlanItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
