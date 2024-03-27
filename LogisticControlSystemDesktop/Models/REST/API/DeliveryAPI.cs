using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class DeliveryAPI : BaseEntityAPI
    {
        public static DeliveryAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public DeliveryAPI() : base()
        {
            ControllerName = "Delivery";
            TypeObject = typeof(Delivery);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
