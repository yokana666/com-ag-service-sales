using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels
{
    public class GarmentBookingOrderViewModel : BaseViewModel, IValidatableObject
    {
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public long BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public long SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public bool IsBlockingPlan { get; set; }
        public bool IsCanceled { get; set; }
        public DateTimeOffset? CanceledDate { get; set; }
        public double CanceledQuantity { get; set; }
        public DateTimeOffset? ExpiredBookingDate { get; set; }
        public double ExpiredBookingQuantity { get; set; }
        public double ConfirmedQuantity { get; set; }
        public bool HadConfirmed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int clientTimeZoneOffset = 0;
            DateTimeOffset dt = DateTimeOffset.Now.AddDays(45);
            dt.ToOffset(new TimeSpan(clientTimeZoneOffset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
            if (SectionName == null)
                yield return new ValidationResult("Seksi harus diisi", new List<string> { "Section" });
            if (BuyerName == null)
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });
            if (this.OrderQuantity <= 0)
                yield return new ValidationResult("Jumlah Order harus lebih besar dari 0", new List<string> { "OrderQuantity" });
            if (this.DeliveryDate == null || this.DeliveryDate == DateTimeOffset.MinValue)
                yield return new ValidationResult("Tanggal Pengiriman harus diisi", new List<string> { "DeliveryDate" });
            else if (this.DeliveryDate < this.BookingOrderDate)
                yield return new ValidationResult("Tanggal Pengiriman Harus lebih dari Tanggal Booking", new List<string> { "DeliveryDate" });
            else if (this.DeliveryDate < DateTimeOffset.Now.AddDays(45) )
                yield return new ValidationResult("Tanggal Pengiriman harus lebih dari "+ dt.ToOffset(new TimeSpan(clientTimeZoneOffset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), new List<string> { "DeliveryDate" });
        }
    }
}
