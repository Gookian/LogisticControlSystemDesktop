using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class VehicleAPI : BaseEntityAPI
    {
        public static VehicleAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public VehicleAPI() : base()
        {
            ControllerName = "Vehicle";
            TypeObject = typeof(Vehicle);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
