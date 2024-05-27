using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class OrderDetailAPI : BaseEntityAPI
    {
        public static OrderDetailAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public OrderDetailAPI() : base()
        {
            ControllerName = "OrderDetail";
            TypeObject = typeof(OrderDetail);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
