using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.Report
{
    public class DailyOperationViewModel
    {
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string color { get; set; }
        public string area { get; set; }
        public string machine { get; set; }
        public string step { get; set; }
    }
}
