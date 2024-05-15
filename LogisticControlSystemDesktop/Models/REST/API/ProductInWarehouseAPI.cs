using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class ProductInWarehouseAPI : BaseEntityAPI
    {
        public static ProductInWarehouseAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public ProductInWarehouseAPI() : base()
        {
            ControllerName = "ProductInWarehouse";
            TypeObject = typeof(ProductInWarehouse);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
