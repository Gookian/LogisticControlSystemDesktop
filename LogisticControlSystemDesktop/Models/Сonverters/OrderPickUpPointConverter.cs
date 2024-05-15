using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class OrderPickUpPointConverter
    {
        public IEnumerable<OrderPickUpPointViewModel> Convert(IEnumerable<OrderPickUpPoint> items)
        {
            List<OrderPickUpPointViewModel> result = new List<OrderPickUpPointViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public OrderPickUpPointViewModel Convert(OrderPickUpPoint item)
        {
            var result = new OrderPickUpPointViewModel()
            {
                Number = item.OrderPickUpPointId,
                Name = item.Name,
                Address = item.Address,
                Latitude = item.Latitude,
                Longitude = item.Longitude
            };

            return result;
        }
    }
}
