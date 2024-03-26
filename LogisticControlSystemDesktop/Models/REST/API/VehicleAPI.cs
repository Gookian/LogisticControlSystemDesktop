using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class VehicleAPI : BaseEntityAPI
    {
        public static VehicleAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public VehicleAPI() : base()
        {
            ControllerName = "Vehicle";
            TypeObject = typeof(Vehicle);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
    /*public class VehicleAPI
    {
        public static IEnumerable<Vehicle> GetAll()
        {
            var httpClient = new HttpClient();
            var baseUrl = "https://localhost:7224/api/Vehicle/";
            httpClient.BaseAddress = new Uri(baseUrl);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string jsonData = "{\"key\":\"value\"}";
            //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsAsync<IEnumerable<Vehicle>>().Result;
            }
        }

        public static Vehicle Delete(int id)
        {
            var httpClient = new HttpClient();
            var baseUrl = $"https://localhost:7224/api/Vehicle/{id}";
            httpClient.BaseAddress = new Uri(baseUrl);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string jsonData = "{\"key\":\"value\"}";
            //StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = httpClient.DeleteAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsAsync<Vehicle>().Result;
            }
        }
    }*/
}
