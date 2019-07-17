using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.Report.OrderStatusReport
{
    public class YearlyOrderQuantity
    {
        public List<int> OrderIds { get; set; }
        public int Month { get; set; }
        public double OrderQuantity { get; set; }
    }
}
