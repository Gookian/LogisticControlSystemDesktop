using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public abstract class BaseEntityAPI
    {
        protected virtual string ControllerName { get; set; }
        protected virtual Type TypeObject { get; set; } = typeof(object);

        protected string baseApiUrl = ConfigurationManager.AppSettings.Get("baseUrl") + "/api/";

        protected BaseEntityAPI() 
        {
        }

        public object GetAll()
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName;
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                Type type = GetIEnumerableTypeWithGeneric(TypeObject);
                dynamic dynamicResult = ReadAsAsyncInvokeWithGenericType(response.Content, type);

                return dynamicResult.Result;
            }
        }

        public object Get(int id)
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName + $"/{id}";
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                dynamic dynamicResult = ReadAsAsyncInvokeWithGenericType(response.Content, TypeObject);

                return dynamicResult.Result;
            }
        }

        public IEnumerable<StructureItem> GetStructure()
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName + $"/Structure";
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsAsync<IEnumerable<StructureItem>>().Result;
            }
        }

        public IEnumerable<IdTargetValueItem> GetIdTargetValues()
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName + $"/IdTargetValue";
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            var response = httpClient.GetAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsAsync<IEnumerable<IdTargetValueItem>>().Result;
            }
        }

        public object Create(object instance)
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName;
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            string jsonData = JsonConvert.SerializeObject(instance);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(baseUrl, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                dynamic dynamicResult = ReadAsAsyncInvokeWithGenericType(response.Content, TypeObject);

                return dynamicResult.Result;
            }
        }

        public object Edit(int id, object instance)
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName + $"/{id}";
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            string jsonData = JsonConvert.SerializeObject(instance);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync(baseUrl, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                dynamic dynamicResult = ReadAsAsyncInvokeWithGenericType(response.Content, TypeObject);

                return dynamicResult.Result;
            }
        }

        public object Delete(int id)
        {
            var httpClient = new HttpClient();
            var baseUrl = baseApiUrl + ControllerName + $"/{id}";
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationAPI.Instance.Token);

            var response = httpClient.DeleteAsync(baseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                dynamic dynamicResult = ReadAsAsyncInvokeWithGenericType(response.Content, TypeObject);

                return dynamicResult.Result;
            }
        }

        private Type GetIEnumerableTypeWithGeneric(Type type)
        {
            Type genericClass = typeof(IEnumerable<>);
            Type constructedClass = genericClass.MakeGenericType(type);

            return constructedClass;
        }

        // response.Content.ReadAsAsync<Type>().Result;
        private dynamic ReadAsAsyncInvokeWithGenericType(HttpContent content, Type type)
        {
            MethodInfo method = typeof(HttpContentExtensions).GetMethod("ReadAsAsync", new Type[] { typeof(HttpContent) });
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            var resultMethod = genericMethod.Invoke(content, new object[] { content });

            dynamic dynamicResult = resultMethod;

            return dynamicResult;
        }
    }
}
