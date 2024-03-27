using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class PackageСontentAPI : BaseEntityAPI
    {
        public static PackageСontentAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public PackageСontentAPI() : base()
        {
            ControllerName = "PackageСontent";
            TypeObject = typeof(PackageСontent);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
