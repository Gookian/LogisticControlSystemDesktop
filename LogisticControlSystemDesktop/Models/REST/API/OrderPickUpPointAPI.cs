﻿using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class OrderPickUpPointAPI : BaseEntityAPI
    {
        public static OrderPickUpPointAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public OrderPickUpPointAPI() : base()
        {
            ControllerName = "OrderPickUpPoint";
            TypeObject = typeof(OrderPickUpPoint);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}