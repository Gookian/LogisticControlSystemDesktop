namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class PackageNotificationHub : ClientNotificationHub<Package>
    {
        public static PackageNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public PackageNotificationHub() : base()
        {
            HubName = "PackageNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
