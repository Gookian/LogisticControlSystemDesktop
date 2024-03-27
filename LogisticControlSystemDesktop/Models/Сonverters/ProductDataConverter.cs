using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class ProductDataConverter
    {
        public IEnumerable<ProductDataViewModel> Convert(IEnumerable<ProductData> items)
        {
            List<ProductDataViewModel> result = new List<ProductDataViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public ProductDataViewModel Convert(ProductData item)
        {
            var result = new ProductDataViewModel()
            {
                Number = item.ProductDataId,
                Name = item.Name,
                Article = item.Article,
                Cost = item.Cost,
            };

            return result;
        }
    }
}
