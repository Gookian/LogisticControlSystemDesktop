using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Windows.Media.Media3D;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class FlightViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public string RegistrationNumber { get; set; }
        public int VehicleId { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public FlightViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var flight = (Flight)FlightAPI.Instance.Get(Number);

            RegistrationNumber = flight.Vehicle.RegistrationNumber;
            VehicleId = flight.VehicleId;

            OnPropertyChanged(nameof(RegistrationNumber));
            OnPropertyChanged(nameof(VehicleId));
        }

        public void Delete_Click()
        {
            var flight = FlightAPI.Instance.Delete(Number) as Flight;
            
            if (flight != null)
            {
                OnDeleted?.Invoke(flight.FlightId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование маршрута", FlightAPI.Instance, typeof(Flight));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            Navigator.Instance.Open(view, "Редактирование маршрута");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
