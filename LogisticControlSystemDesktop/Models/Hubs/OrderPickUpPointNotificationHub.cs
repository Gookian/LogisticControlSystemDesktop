namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class OrderPickUpPointNotificationHub : ClientNotificationHub<OrderPickUpPoint>
    {
        public static OrderPickUpPointNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public OrderPickUpPointNotificationHub() : base()
        {
            HubName = "OrderPickUpPointNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
