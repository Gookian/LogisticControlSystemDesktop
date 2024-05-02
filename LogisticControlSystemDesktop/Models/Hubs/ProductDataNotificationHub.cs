namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class ProductDataNotificationHub : ClientNotificationHub<ProductData>
    {
        public static ProductDataNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public ProductDataNotificationHub() : base()
        {
            HubName = "ProductDataNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
