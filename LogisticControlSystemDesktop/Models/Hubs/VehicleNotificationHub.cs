namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class VehicleNotificationHub : ClientNotificationHub<Vehicle>
    {
        public static VehicleNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public VehicleNotificationHub() : base()
        {
            HubName = "VehicleNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
