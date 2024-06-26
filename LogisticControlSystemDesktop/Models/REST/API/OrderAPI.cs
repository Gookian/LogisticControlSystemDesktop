﻿using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class OrderAPI : BaseEntityAPI
    {
        public static OrderAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public OrderAPI() : base()
        {
            ControllerName = "Order";
            TypeObject = typeof(Order);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
