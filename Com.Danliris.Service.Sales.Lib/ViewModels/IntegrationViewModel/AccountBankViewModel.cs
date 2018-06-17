using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class AccountBankViewModel
    {
        public long Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        [MaxLength(255)]
        public string BankName { get; set; }
        [MaxLength(255)]
        public string AccountCurrencyId { get; set; }
        [MaxLength(255)]
        public string AccountCurrencyCode { get; set; }
        public CurrencyViewModel Currency { get; set; }
    }
}
