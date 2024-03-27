using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class ProductAPI : BaseEntityAPI
    {
        public static ProductAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public ProductAPI() : base()
        {
            ControllerName = "Product";
            TypeObject = typeof(Product);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
