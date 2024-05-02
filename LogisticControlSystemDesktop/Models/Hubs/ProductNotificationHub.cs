namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class ProductNotificationHub : ClientNotificationHub<Product>
    {
        public static ProductNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public ProductNotificationHub() : base()
        {
            HubName = "ProductNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
