using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class FlightConverter
    {
        public IEnumerable<FlightViewModel> Convert(IEnumerable<Flight> items)
        {
            List<FlightViewModel> result = new List<FlightViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public FlightViewModel Convert(Flight item)
        {
            var vehicle = (Vehicle)VehicleAPI.Instance.Get(item.VehicleId);

            var result = new FlightViewModel()
            {
                Number = item.FlightId,
                FlightNumber = item.Number,
                VehicleId = item.VehicleId,
                RegistrationNumber = vehicle.RegistrationNumber,
            };

            return result;
        }
    }
}
