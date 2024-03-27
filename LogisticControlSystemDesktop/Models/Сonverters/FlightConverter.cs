using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
                VehicleId = item.VehicleId,
                RegistrationNumber = vehicle.RegistrationNumber,
            };

            return result;
        }
    }
}
