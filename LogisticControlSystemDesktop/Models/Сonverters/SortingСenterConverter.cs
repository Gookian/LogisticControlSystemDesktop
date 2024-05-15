using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class SortingСenterConverter
    {
        public IEnumerable<SortingСenterViewModel> Convert(IEnumerable<SortingСenter> items)
        {
            List<SortingСenterViewModel> result = new List<SortingСenterViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public SortingСenterViewModel Convert(SortingСenter item)
        {
            var result = new SortingСenterViewModel()
            {
                Number = item.SortingСenterId,
                Name = item.Name,
                Address = item.Address,
                Latitude = item.Latitude,
                Longitude = item.Longitude
            };

            return result;
        }
    }
}
