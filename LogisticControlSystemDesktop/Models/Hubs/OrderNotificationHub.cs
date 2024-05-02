namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class OrderNotificationHub : ClientNotificationHub<Order>
    {
        public static OrderNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public OrderNotificationHub() : base()
        {
            HubName = "OrderNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
