using System.Net.Http;
using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class AuthenticationAPI
    {
        public static AuthenticationAPI Instance { get; set; }

        public string Token { get; set; }

        private string _controllerName = "Authentication";

        private string _baseApiUrl = "http://192.168.1.6:5115/api/";//"https://localhost:7141/api/";

        public AuthenticationAPI() : base()
        {
            if (Instance != this)
            {
                Instance = this;
            }
        }

        public Guid? Authentication(string username, string password)
        {
            var httpClient = new HttpClient();
            var baseUrl = _baseApiUrl + _controllerName + $"?username={username}&password={password}";
            httpClient.BaseAddress = new Uri(baseUrl);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                Token = "";
                return null;
            }
            else
            {
                var result = response.Content.ReadAsAsync<Guid>().Result;

                Token = result.ToString();
                return result;
            }
        }
    }
}
