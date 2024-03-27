using LogisticControlSystemDesktop.REST.API;
using System.Collections.Generic;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class VehicleConverter
    {
        private BrushConverter _converter = new BrushConverter();

        public IEnumerable<VehicleViewModel> Convert(IEnumerable<Vehicle> Items)
        {
            List<VehicleViewModel> vehicleDatas = new List<VehicleViewModel>();

            foreach (var item in Items)
            {
                vehicleDatas.Add(Convert(item));
            }

            return vehicleDatas;
        }

        public VehicleViewModel Convert(Vehicle item)
        {
            var result = new VehicleViewModel()
            {
                Number = item.VehicleId,
                Brand = item.Brand,
                RegistrationNumber = item.RegistrationNumber,
                Capacity = item.Capacity + " м.кв.",
                LoadCapacity = item.LoadCapacity + " тон",
                Type = item.Type,
                Name = item.Name,
                Character = item.Name.Substring(0, 1),
                BgColor = (SolidColorBrush)_converter.ConvertFromString("#f28d82")
            };

            return result;
        }
    }
}
