using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class PackageContentAPI : BaseEntityAPI
    {
        public static PackageContentAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public PackageContentAPI() : base()
        {
            ControllerName = "PackageСontent";
            TypeObject = typeof(PackageContent);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
