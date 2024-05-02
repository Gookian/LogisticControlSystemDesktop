namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class FlightNotificationHub : ClientNotificationHub<Flight>
    {
        public static FlightNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public FlightNotificationHub() : base()
        {
            HubName = "FlightNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
