using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SecurityRepository
    {
        private readonly HttpClient _httpClient;

        public SecurityRepository(Uri baseAddress)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Default
            };

            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = baseAddress;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public KeyInfo Get()
        {
            Task<HttpResponseMessage> httpResponseMessageTask = _httpClient.GetAsync("security");            
            httpResponseMessageTask.Wait();
            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;
            httpResponseMessage.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<KeyInfo>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }
    }
}
