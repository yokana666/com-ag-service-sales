using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.Garment
{
    public class BudgetJobOrderDisplayViewModel
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public double BudgetQuantity { get; set; }
        public string UomPriceName { get; set; }
        public double Price { get; set; }
        public string POSerialNumber { get; set; }
    }
}
