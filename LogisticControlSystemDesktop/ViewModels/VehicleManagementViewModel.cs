using LogisticControlSystemDesktop.Models;
//using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class VehicleManagementViewModel : BindableBase
    {
        public ObservableCollection<VehicleData> Vihecles { get; set; }

        public VehicleManagementViewModel()
        {
            Vihecles = new ObservableCollection<VehicleData>();

            var converter = new BrushConverter();
            //var vehicle = VehicleAPI.GetAll();
            Vihecles.Add(new VehicleData()
            {
                Number = 1,
                BgColor = (Brush)converter.ConvertFromString("#1098ad"),
                Character = "И",
                Name = "Иванов Иван Иванович",
                Type = "Грузовой",
                Brand = "Scania",
                RegistrationNumber = "РС234А70",
                Capacity = "20 кв.м.",
                LoadCapacity = "5 тон"
            });
        }
    }
}
