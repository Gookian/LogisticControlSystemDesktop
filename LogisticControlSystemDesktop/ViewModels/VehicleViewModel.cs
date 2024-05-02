using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.Models
{
    public class VehicleViewModel : BindableBase
    {
        public int Number { get; set; }
        public Brush BgColor { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string RegistrationNumber { get; set; }
        public string Capacity { get; set; }
        public string LoadCapacity { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public VehicleViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        public void Delete_Click()
        {
            VehicleAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование транспортного средства", VehicleAPI.Instance, typeof(Vehicle));

            MainNavigator.Instance.Open(view, "Редактирование ТС");
        }
    }
}
