using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface
{
    public interface IBaseFacade<TModel>
    {
        Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int Page, int Size, string Order, List<string> Select, string Keyword, string Filter);
        Task<int> CreateAsync(TModel model);
        Task<TModel> ReadByIdAsync(int id);
        Task<int> UpdateAsync(int id, TModel model);
        Task<int> DeleteAsync(int id);
    }
}
