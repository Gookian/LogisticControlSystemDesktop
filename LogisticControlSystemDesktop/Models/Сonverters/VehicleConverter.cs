using LogisticControlSystemDesktop.REST.API;
using System.Collections.Generic;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class VehicleConverter
    {
        private BrushConverter _converter = new BrushConverter();

        public IEnumerable<VehicleViewModel> Convert(IEnumerable<Vehicle> vehicles)
        {
            List<VehicleViewModel> vehicleDatas = new List<VehicleViewModel>();

            foreach (var vehicle in vehicles)
            {
                vehicleDatas.Add(new VehicleViewModel()
                {
                    Number = vehicle.VehicleId,
                    Brand = vehicle.Brand,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    Capacity = vehicle.Capacity + " м.кв.",
                    LoadCapacity = vehicle.LoadCapacity + " тон",
                    Type = "",
                    Name = "",
                    Character = "",
                    BgColor = (SolidColorBrush)_converter.ConvertFromString("#f28d82")
                });
            }

            return vehicleDatas;
        }
    }
}
