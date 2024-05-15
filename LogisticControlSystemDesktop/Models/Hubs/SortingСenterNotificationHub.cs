namespace LogisticControlSystemDesktop.Models.Hubs
{
    public class SortingСenterNotificationHub : ClientNotificationHub<SortingСenter>
    {
        public static SortingСenterNotificationHub Instance { get; set; }

        protected override string HubName { get => base.HubName; set => base.HubName = value; }

        public SortingСenterNotificationHub() : base()
        {
            HubName = "SortingСenterNotificationHub";

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
