using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class WarehouseAPI : BaseEntityAPI
    {
        public static WarehouseAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public WarehouseAPI() : base()
        {
            ControllerName = "Warehouse";
            TypeObject = typeof(Warehouse);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
