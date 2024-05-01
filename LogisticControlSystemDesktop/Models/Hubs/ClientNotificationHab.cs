using System.Net.Http;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using LogisticControlSystemDesktop.Models.REST.API;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticControlSystemDesktop.Models.Hubs
{
    public abstract class ClientNotificationHab<TEntity>
    {
        protected HubConnection _connection;

        protected virtual string HubName { get; set; }

        protected string baseApiUrl = "https://localhost:7141";

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
