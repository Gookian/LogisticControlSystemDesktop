namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class VehicleNotificationHab : ClientNotificationHab<Vehicle>
    {
        public static VehicleNotificationHab Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public VehicleNotificationHab() : base()
        {
            HubName = "VehicleNotificationHab";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
