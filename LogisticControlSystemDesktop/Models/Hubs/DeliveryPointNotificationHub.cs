namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class DeliveryPointNotificationHub : ClientNotificationHub<DeliveryPoint>
    {
        public static DeliveryPointNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public DeliveryPointNotificationHub() : base()
        {
            HubName = "DeliveryPointNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
