using System;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting
{
    public class FinishingPrintingSalesContractReportViewModel
    {
        public int No { get; set; }
        public string SalesContractNo { get; set; }
        public DateTime SalesContractDate { get; set; }
        public string BuyerName { get; set; }
        public string BuyerType { get; set; }
        public string DispositionNumber { get; set; }
        public string OrderType { get; set; }
        public string Commodity { get; set; }
        public string Quality { get; set; }
        public double OrderQuantity { get; set; }
    }
}
