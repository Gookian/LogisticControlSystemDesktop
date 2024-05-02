namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class WarehouseNotificationHub : ClientNotificationHub<Warehouse>
    {
        public static WarehouseNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public WarehouseNotificationHub() : base()
        {
            HubName = "WarehouseNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
