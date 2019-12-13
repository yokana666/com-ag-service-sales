using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(25)]
        public string Code { get; set; }
        [MaxLength(25)]
        public string SalesInvoiceNo { get; set; }
        public DateTimeOffset SalesInvoiceDate { get; set; }
        [MaxLength(25)]
        public string DeliveryOrderNo { get; set; }

        /*DO Sales*/
        public int? DOSalesId { get; set; }
        [MaxLength(25)]
        public string DOSalesNo { get; set; }

        /*Buyer*/
        public int? BuyerId { get; set; }
        [MaxLength(250)]
        public string BuyerName { get; set; }
        [MaxLength(100)]
        public string BuyerNPWP { get; set; }

        /*Currency*/
        public int? CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }


        [MaxLength(100)]
        public string NPWP { get; set; }
        [MaxLength(100)]
        public string NPPKP { get; set; }
        [MaxLength(25)]
        public string DebtorIndexNo { get; set; }
        public DateTimeOffset DueDate { get; set; }
        [MaxLength(25)]
        public string Disp { get; set; }
        [MaxLength(25)]
        public string Op { get; set; }
        [MaxLength(25)]
        public string Sc { get; set; }
        public bool? UseVat { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }

        public ICollection<SalesInvoiceDetailViewModel> SalesInvoiceDetails { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SalesInvoiceNo))
                yield return new ValidationResult("No. Faktur Penjualan harus diisi", new List<string> { "SalesInvoiceNo" });

            if (SalesInvoiceDate == null || SalesInvoiceDate > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl faktur penjualan harus diisi & lebih kecil sama dengan hari ini", new List<string> { "SalesInvoiceDate" });

            if (string.IsNullOrWhiteSpace(DeliveryOrderNo))
                yield return new ValidationResult("No. Surat Jalan harus diisi", new List<string> { "DeliveryOrderNo" });

            if (string.IsNullOrWhiteSpace(DOSalesNo))
            {
                yield return new ValidationResult("DO Penjualan harus di isi", new List<string> { "DOSalesNo" });
            }

            if (string.IsNullOrWhiteSpace(CurrencyCode))
            {
                yield return new ValidationResult("Kurs harus di isi", new List<string> { "CurrencyCode" });
            }

            if (string.IsNullOrWhiteSpace(BuyerName))
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "BuyerName" });

            if (string.IsNullOrWhiteSpace(BuyerNPWP))
                yield return new ValidationResult("NPWP harus diisi", new List<string> { "BuyerNPWP" });

            if (string.IsNullOrWhiteSpace(NPWP))
                yield return new ValidationResult("NPWP harus diisi", new List<string> { "NPWP" });

            if (string.IsNullOrWhiteSpace(NPPKP))
                yield return new ValidationResult("NPPKP harus diisi", new List<string> { "NPPKP" });

            if (string.IsNullOrWhiteSpace(DebtorIndexNo))
                yield return new ValidationResult("No. Index Debitur harus diisi", new List<string> { "DebtorIndexNo" });

            if (DueDate == null || DueDate <= DateTimeOffset.Now)
                yield return new ValidationResult("Tanggal jatuh tempo harus diisi & lebih besar sama dengan hari ini", new List<string> { "DueDate" });

            if (string.IsNullOrWhiteSpace(Disp))
                yield return new ValidationResult("Disp harus diisi", new List<string> { "Disp" });

            if (string.IsNullOrWhiteSpace(Op))
                yield return new ValidationResult("Op harus diisi", new List<string> { "Op" });

            if (string.IsNullOrWhiteSpace(Sc))
                yield return new ValidationResult("Sc harus diisi", new List<string> { "Sc" });

            int Count = 0;
            string DetailErrors = "[";

            if (SalesInvoiceDetails != null && SalesInvoiceDetails.Count > 0)
            {
                foreach (SalesInvoiceDetailViewModel detail in SalesInvoiceDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (string.IsNullOrWhiteSpace(detail.UnitCode))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitCode : 'Kode harus diisi',";
                    }
                    if (string.IsNullOrWhiteSpace(detail.Quantity))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Quantity : 'Kuantitas harus diisi',";
                    }
                    if (!detail.Total.HasValue || detail.Total.Value <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Total : 'Jumlah harus lebih besar dari 0 & tidak boleh kosong',";
                    }
                    if (string.IsNullOrWhiteSpace(detail.UomUnit))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UomUnit : 'Satuan harus diisi',";
                    }
                    if (string.IsNullOrWhiteSpace(detail.UnitName))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitName : 'Nama barang harus diisi',";
                    }
                    if (!detail.UnitPrice.HasValue || detail.UnitPrice.Value <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitPrice : 'Harga barang harus lebih besar dari 0 & tidak boleh kosong',";
                    }
                    if (detail.Amount <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Amount : 'Jumlah & Harga Satuan harus diisi',";
                    }

                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = SalesInvoiceDetails.Where(f =>
                                f.UnitCode.Equals(detail.UnitCode) &&
                                f.UnitName.Equals(detail.UnitName) &&
                                f.UnitPrice.GetValueOrDefault().Equals(detail.UnitPrice.GetValueOrDefault()) &&
                                f.UomId.GetValueOrDefault().Equals(detail.UomId.GetValueOrDefault())
                            ).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "UnitCode : 'Kode, Nama Barang, dan Harga Satuan tidak boleh duplikat',";
                            DetailErrors += "UnitName : 'Kode, Nama Barang, dan Harga Satuan tidak boleh duplikat',";
                            DetailErrors += "UnitPrice : 'Kode, Nama Barang, dan Harga Satuan tidak boleh duplikat',";
                        }
                    }
                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "SalesInvoiceDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "SalesInvoiceDetails" });

        }
    }
}
