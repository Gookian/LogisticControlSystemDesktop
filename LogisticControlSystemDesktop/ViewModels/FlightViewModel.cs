using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class FlightViewModel : BindableBase
    {
        public int Number { get; set; }
        public string FlightNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public int VehicleId { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public FlightViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        public void Delete_Click()
        {
            FlightAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование маршрута", FlightAPI.Instance, typeof(Flight));

            MainNavigator.Instance.Open(view, "Редактирование маршрута");
        }
    }
}
