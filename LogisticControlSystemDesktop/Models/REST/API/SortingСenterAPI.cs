using System;

namespace LogisticControlSystemDesktop.Models.REST.API
{
    public class SortingСenterAPI : BaseEntityAPI
    {
        public static SortingСenterAPI Instance { get; set; }

        protected override string ControllerName { get => base.ControllerName; set => base.ControllerName = value; }
        protected override Type TypeObject { get => base.TypeObject; set => base.TypeObject = value; }

        public SortingСenterAPI() : base()
        {
            ControllerName = "SortingСenter";
            TypeObject = typeof(SortingСenter);

            if (Instance != this)
            {
                Instance = this;
            }
        }
    }
}