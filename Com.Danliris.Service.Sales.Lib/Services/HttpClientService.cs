using Com.Danliris.Service.Sales.Lib.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.Services
{
    class HttpClientService : IHttpClientService
    {
        private HttpClient _client = new HttpClient();

        public HttpClientService(IdentityService identityService)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, identityService.Token);
        }

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return await _client.PutAsync(url, content);
        }
    }
}
