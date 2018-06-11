using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.Weaving
{
    public class WeavingSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string SalesContractNo { get; set; }
        [MaxLength(255)]
        public string DispositionNumber { get; set; }
        public bool FromStock { get; set; }
        [MaxLength(255)]
        public string MaterialWidth { get; set; }
        public double OrderQuantity { get; set; }
        public double ShippingQuantityTolerance { get; set; }
        public string ComodityDescription { get; set; }
        [MaxLength(255)]
        public string IncomeTax { get; set; }
        [MaxLength(1000)]
        public string TermOfShipment { get; set; }
        [MaxLength(1000)]
        public string TransportFee { get; set; }
        [MaxLength(1000)]
        public string Packing { get; set; }
        [MaxLength(1000)]
        public string DeliveredTo { get; set; }
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Comission { get; set; }
        public DateTimeOffset DeliverySchedule { get; set; }
        public string ShipmentDescription { get; set; }
        [MaxLength(1000)]
        public string Condition { get; set; }
        public string Remark { get; set; }
        [MaxLength(1000)]
        public string PieceLength { get; set; }

        public BuyerViewModel Buyer { get; set; }
        public ProductViewModel Product { get; set; }
        public UomViewModel Uom { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        public YarnMaterialViewModel YarnMaterial { get; set; }
        public ComodityViewModel Comodity { get; set; }
        public QualityViewModel Quality { get; set; }
        public TermPaymentViewModel TermPayment { get; set; }
        public AccountBankViewModel AccountBank { get; set; }
        public AgentViewModel Agent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (this.Buyer == null || this.Buyer.Id.Equals(0))
                yield return new ValidationResult("Buyer harus di isi", new List<string> { "Buyer" });
            if (this.Product == null || this.Product.Id.Equals(0))
                yield return new ValidationResult("Product harus di isi", new List<string> { "Product" });
            if (this.Uom == null || this.Uom.Id.Equals(0))
                yield return new ValidationResult("Uom harus di isi", new List<string> { "Uom" });
            if (this.MaterialConstruction == null || this.MaterialConstruction.Id.Equals(0))
                yield return new ValidationResult("MaterialConstruction harus di isi", new List<string> { "MaterialConstruction" });
            if (this.YarnMaterial == null || this.YarnMaterial.Id.Equals(0))
                yield return new ValidationResult("YarnMaterial harus di isi", new List<string> { "YarnMaterial" });
            if (this.Comodity == null || this.Comodity.Id.Equals(0))
                yield return new ValidationResult("Comodity harus di isi", new List<string> { "Comodity" });
            if (this.Quality == null || this.Quality.Id.Equals(0))
                yield return new ValidationResult("Quality harus di isi", new List<string> { "Quality" });
            if (this.TermPayment == null || this.TermPayment.Id.Equals(0))
                yield return new ValidationResult("TermPayment harus di isi", new List<string> { "TermPayment" });
            if (this.AccountBank == null || this.AccountBank.Id.Equals(0))
                yield return new ValidationResult("AccountBank harus di isi", new List<string> { "AccountBank" });
            if (this.Agent == null || this.Agent.Id.Equals(0))
                yield return new ValidationResult("Agent harus di isi", new List<string> { "Agent" });

            if (string.IsNullOrWhiteSpace(this.DispositionNumber))
                yield return new ValidationResult("DispositionNumber harus lebih di isi", new List<string> { "DispositionNumber" });

            //if (this.Operator == null || string.IsNullOrWhiteSpace(this.Operator))
            //    yield return new ValidationResult("Operator harus di isi", new List<string> { "Operator" });

            //if (this.Date == null)
            //    yield return new ValidationResult("Tanggal harus di isi", new List<string> { "Date" });

        }
    }
}
