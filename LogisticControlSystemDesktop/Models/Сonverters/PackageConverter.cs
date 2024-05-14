using LogisticControlSystemDesktop.ViewModels;
using System.Collections.Generic;

namespace LogisticControlSystemDesktop.Models.converters
{
    public class PackageConverter
    {
        public IEnumerable<PackageViewModel> Convert(IEnumerable<Package> items)
        {
            List<PackageViewModel> result = new List<PackageViewModel>();

            foreach (var item in items)
            {
                result.Add(Convert(item));
            }

            return result;
        }

        public PackageViewModel Convert(Package item)
        {
            var result = new PackageViewModel()
            {
                Number = item.PackageId,
                StateName = item.PackageState.Name,
                PackageNumber = item.Number,
                BuildDeadline = item.BuildDeadline.ToString("dd.MM.yyyy HH:mm:ss")
            };

            return result;
        }
    }
}
