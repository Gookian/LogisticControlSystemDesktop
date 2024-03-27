using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class PackageStateAPI : BaseEntityAPI
    {
        public static PackageStateAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public PackageStateAPI() : base()
        {
            ControllerName = "PackageState";
            TypeObject = typeof(PackageState);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
