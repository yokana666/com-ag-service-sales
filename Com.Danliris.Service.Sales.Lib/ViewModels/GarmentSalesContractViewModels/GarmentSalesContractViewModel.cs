using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels
{
    public class GarmentSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public string SalesContractNo { get; set; }
        public int CostCalculationId { get; set; }
        public string RONumber { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string ComodityId { get; set; }
        public string Comodity { get; set; }
        public string ComodityCode { get; set; }
        public string Packing { get; set; }
        public string Article { get; set; }
        public double Quantity { get; set; }

        public UomViewModel Uom { get; set; }

        public string Description { get; set; }
        public string Material { get; set; }
        public string DocPresented { get; set; }
        public string FOB { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string Delivery { get; set; }
        public string Country { get; set; }
        public string NoHS { get; set; }
        public bool IsMaterial { get; set; }
        public bool IsTrimming { get; set; }
        public bool IsEmbrodiary { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsWash { get; set; }
        public bool IsTTPayment { get; set; }
        public string PaymentDetail { get; set; }
        public AccountBankViewModel AccountBank { get; set; }
        public bool DocPrinted { get; set; }
        public List<GarmentSalesContractItemViewModel> Items { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(RONumber))
            {
                yield return new ValidationResult("Nomor RO harus diisi", new List<string> { "RONumber" });
            }
            
            if (string.IsNullOrWhiteSpace(Delivery))
            {
                yield return new ValidationResult("Delivery harus diisi", new List<string> { "Delivery" });
            }

            if (AccountBank == null || AccountBank.Id.Equals(0))
            {
                yield return new ValidationResult("Bank Detail harus di isi", new List<string> { "AccountBankId" });
            }

            if (string.IsNullOrWhiteSpace(NoHS))
            {
                yield return new ValidationResult("NoHS harus diisi", new List<string> { "NoHS" });
            }
            if (string.IsNullOrWhiteSpace(Packing))
            {
                yield return new ValidationResult("Packing harus diisi", new List<string> { "Packing" });
            }
            if (string.IsNullOrWhiteSpace(Country))
            {
                yield return new ValidationResult("Destination harus diisi", new List<string> { "Country" });
            }
            if (string.IsNullOrWhiteSpace(FOB))
            {
                yield return new ValidationResult("FOB harus diisi", new List<string> { "FOB" });
            }
            if (string.IsNullOrWhiteSpace(DocPresented))
            {
                yield return new ValidationResult("DocPresented harus diisi", new List<string> { "DocPresented" });
            }
            if (string.IsNullOrWhiteSpace(Material))
            {
                yield return new ValidationResult("Material harus diisi", new List<string> { "Material" });
            }
            if (Price<=0)
            {
                if(Items.Count==0)
                    yield return new ValidationResult("Price harus diisi", new List<string> { "Price" });
            }

            if (Items.Count > 0)
            {
                int Count = 0;
                string ItemError = "[";

                foreach (GarmentSalesContractItemViewModel item in Items)
                {
                    ItemError += "{";
                    if (string.IsNullOrWhiteSpace(item.Description))
                    {
                        Count++;
                        ItemError += " Description: 'Keterangan harus diisi' , ";
                    }

                    if (item.Price <= 0)
                    {
                        Count++;
                        ItemError += " Quantity: 'Quantity harus lebih besar dari 0' , ";
                    }

                    if (item.Price <= 0)
                    {
                        Count++;
                        ItemError += " Price: 'Harga harus lebih besar dari 0' , ";
                    }
                    ItemError += "}, ";
                }

                ItemError += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(ItemError, new List<string> { "Items" });
                }
            }
        }
    }
}
