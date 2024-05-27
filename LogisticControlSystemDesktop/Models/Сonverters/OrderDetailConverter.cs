using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class OrderDetailConverter
    {
        public IEnumerable<OrderDetailViewModel> Convert(IEnumerable<OrderDetail> items)
        {
            List<OrderDetailViewModel> result = new List<OrderDetailViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public OrderDetailViewModel Convert(OrderDetail item)
        {
            var result = new OrderDetailViewModel()
            {
                Number = item.OrderDetailId,
                ProductName = item.ProductData.Name,
                Count = item.Count + " шт",
                Sum = item.ProductData.Cost * item.Count + " руб",
            };

            return result;
        }
    }
}
