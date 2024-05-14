using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class OrderConverter
    {
        public IEnumerable<OrderViewModel> Convert(IEnumerable<Order> items)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public OrderViewModel Convert(Order item)
        {
            var result = new OrderViewModel()
            {
                Number = item.OrderId,
                FirstName = item.FirstName,
                MiddleName = item.MiddleName,
                Surname = item.Surname,
                Address = item.Address,
                DeliveryDateTime = item.DeliveryDateTime.ToString("dd.MM.yyyy HH:mm:ss"),
            };

            return result;
        }
    }
}
