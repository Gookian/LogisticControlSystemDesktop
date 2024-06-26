﻿using System;
using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;

namespace LogisticControlSystemDesktop.REST.API
{
    public class PackageAPI : BaseEntityAPI
    {
        public static PackageAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public PackageAPI() : base()
        {
            ControllerName = "Package";
            TypeObject = typeof(Package);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}
