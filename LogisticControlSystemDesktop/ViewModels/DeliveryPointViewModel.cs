using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class DeliveryPointViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public DeliveryPointViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var deliveryPoint = (DeliveryPoint)DeliveryPointAPI.Instance.Get(Number);

            Name = deliveryPoint.Name;
            Address = deliveryPoint.Address;
            Latitude = deliveryPoint.Latitude;
            Longitude = deliveryPoint.Longitude;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Latitude));
            OnPropertyChanged(nameof(Longitude));
        }

        public void Delete_Click()
        {
            var deliveryPoint = DeliveryPointAPI.Instance.Delete(Number) as DeliveryPoint;
            
            if (deliveryPoint != null)
            {
                OnDeleted?.Invoke(deliveryPoint.DeliveryPointId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование точки доставки", DeliveryPointAPI.Instance, typeof(DeliveryPoint));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            MainNavigator.Instance.Open(view, "Редактирование точки доставки");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
