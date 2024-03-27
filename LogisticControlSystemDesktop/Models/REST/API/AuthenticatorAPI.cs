using System.Net.Http;
using System;
using System.Security.Cryptography.X509Certificates;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class AuthenticatorAPI
    {
        private string _controllerName = "Authenticator";

        private string _baseApiUrl = "https://localhost:7224/api/";

        public object Get(int id)
        {
            var httpClient = new HttpClient();
            var baseUrl = _baseApiUrl + _controllerName + $"/{id}";
            httpClient.BaseAddress = new Uri(baseUrl);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsAsync<Guid>().Result;
            }
        }
    }
}
