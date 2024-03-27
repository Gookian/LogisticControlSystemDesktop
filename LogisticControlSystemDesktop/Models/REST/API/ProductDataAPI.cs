using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class ProductDataAPI : BaseEntityAPI
    {
        public static ProductDataAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public ProductDataAPI() : base()
        {
            ControllerName = "ProductData";
            TypeObject = typeof(ProductData);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
