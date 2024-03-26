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
                result.Add(new WarehouseViewModel()
                {
                    Number = item.WarehouseId,
                    Name = item.Name,
                    Address = item.Address
                });
            }

            return result;
        }
    }
}
