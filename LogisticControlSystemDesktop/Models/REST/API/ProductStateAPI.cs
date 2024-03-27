using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class ProductStateAPI : BaseEntityAPI
    {
        public static ProductStateAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public ProductStateAPI() : base()
        {
            ControllerName = "ProductState";
            TypeObject = typeof(ProductState);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
