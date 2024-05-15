using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class DeliveryPointAPI : BaseEntityAPI
    {
        public static DeliveryPointAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public DeliveryPointAPI() : base()
        {
            ControllerName = "DeliveryPoint";
            TypeObject = typeof(DeliveryPoint);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
