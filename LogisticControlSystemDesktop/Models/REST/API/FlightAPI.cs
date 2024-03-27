using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class FlightAPI : BaseEntityAPI
    {
        public static FlightAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public FlightAPI() : base()
        {
            ControllerName = "Flight";
            TypeObject = typeof(Flight);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
