using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class ProductConverter
    {
        public IEnumerable<ProductViewModel> Convert(IEnumerable<Product> items)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public ProductViewModel Convert(Product item)
        {
            var result = new ProductViewModel()
            {
                Number = item.ProductId,
                StateName = item.ProductState.Name,
                DataName = item.ProductData.Name,
                ProductDataId = item.ProductDataId
            };

            return result;
        }
    }
}
