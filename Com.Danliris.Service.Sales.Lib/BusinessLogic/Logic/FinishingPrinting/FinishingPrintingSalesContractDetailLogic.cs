using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting
{
    public class FinishingPrintingSalesContractDetailLogic : BaseLogic<FinishingPrintingSalesContractDetailModel>
    {
        public FinishingPrintingSalesContractDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService,serviceProvider, dbContext)
        {
        }

        public override ReadResponse<FinishingPrintingSalesContractDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<long> GetFPSalesContractIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.FinishingPrintingSalesContract.Id == id).Select(d => d.Id));
        }
    }
}
