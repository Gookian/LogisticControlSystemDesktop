using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class WarehouseConverter
    {
        public IEnumerable<WarehouseViewModel> Convert(IEnumerable<Warehouse> items)
        {
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public WarehouseViewModel Convert(Warehouse item)
        {
            var result = new WarehouseViewModel()
            {
                Number = item.WarehouseId,
                Name = item.Name,
                Address = item.Address
            };

            return result;
        }
    }
}
