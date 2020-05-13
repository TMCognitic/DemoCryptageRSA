using Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Repository
{
    public class AuthRepository
    {
        private readonly HttpClient _httpClient;

        public AuthRepository(Uri baseAddress)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12
            };

            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = baseAddress;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Register(RegisterForm registerForm)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(registerForm));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("auth/register", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
