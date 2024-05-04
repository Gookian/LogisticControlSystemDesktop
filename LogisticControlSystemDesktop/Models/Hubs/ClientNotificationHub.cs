using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;
using LogisticControlSystemDesktop.Models.REST.API;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace LogisticControlSystemDesktop.Models.Hubs
{
    public abstract class ClientNotificationHub<TEntity>
    {
        protected HubConnection _connection;

        protected virtual string HubName { get; set; }

        protected string baseApiUrl = ConfigurationManager.AppSettings.Get("baseUrl");

        public delegate void EventNotificationHandler(TEntity entity, UpdateType type);
        public event EventNotificationHandler OnReceivedNotification;

        public async Task ConnectAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{baseApiUrl}/{HubName}", options =>
                {
                    options.Headers.Add("Authorization", AuthenticationAPI.Instance.Token);
                })
                .Build();

            _connection.On<TEntity, UpdateType>("NotificationCallback", NotificationCallback);

            try
            {
                await _connection.StartAsync();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Не удалось подключиться к хабу");
            }
        }

        protected void NotificationCallback(TEntity entity, UpdateType type)
        {
            OnReceivedNotification?.Invoke(entity, type);
        }
    }
}
