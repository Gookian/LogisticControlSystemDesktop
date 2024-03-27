using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class DeliveryPointConverter
    {
        public IEnumerable<DeliveryPointViewModel> Convert(IEnumerable<DeliveryPoint> items)
        {
            List<DeliveryPointViewModel> result = new List<DeliveryPointViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public DeliveryPointViewModel Convert(DeliveryPoint item)
        {
            var result = new DeliveryPointViewModel()
            {
                Number = item.DeliveryPointId,
                Name = item.Name,
                Address = item.Address,
                Latitude = item.Latitude,
                Longitude = item.Longitude
            };

            return result;
        }
    }
}
