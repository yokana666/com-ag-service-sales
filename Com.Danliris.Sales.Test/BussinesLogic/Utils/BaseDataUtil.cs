using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.Utils
{
    public abstract class BaseDataUtil<TFacade, TModel>
        where TFacade : IBaseFacade<TModel>
        where TModel : BaseModel
    {
        private TFacade _facade;
        public BaseDataUtil(TFacade facade)
        {
            _facade = facade;
        }

        public virtual TModel GetNewData()
        {
            return Activator.CreateInstance(typeof(TModel)) as TModel;
        }

        public async Task<TModel> GetTestData()
        {
            var data = GetNewData();
            await _facade.CreateAsync(data);
            return data;
        }
    }
}
