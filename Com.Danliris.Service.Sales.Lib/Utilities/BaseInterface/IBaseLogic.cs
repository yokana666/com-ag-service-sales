using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface
{
    public interface IBaseLogic<TModel>
    {
        Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        void Create(TModel model);
        Task<TModel> ReadByIdAsync(int id);
        void Update(int id, TModel model);
        Task DeleteAsync(int id);
    }
}
