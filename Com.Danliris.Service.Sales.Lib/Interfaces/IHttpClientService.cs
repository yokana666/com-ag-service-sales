using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.Interfaces
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
    }
}
