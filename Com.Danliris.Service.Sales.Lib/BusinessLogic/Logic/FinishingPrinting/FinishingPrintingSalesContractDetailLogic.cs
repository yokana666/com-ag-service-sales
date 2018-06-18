using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class FinishingPrintingSalesContractDetailLogic : BaseLogic<FinishingPrintingSalesContractDetailModel>
    {
        public FinishingPrintingSalesContractDetailLogic(IServiceProvider serviceProvider, SalesDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<FinishingPrintingSalesContractDetailModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }
    }
}
