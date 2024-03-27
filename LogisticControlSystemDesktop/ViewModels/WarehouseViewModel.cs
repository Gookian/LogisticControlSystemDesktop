using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class WarehouseViewModel : BindableBase, INotifyPropertyChanged
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

        public WarehouseViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var warehouse = (Warehouse)WarehouseAPI.Instance.Get(Number);

            Name = warehouse.Name;
            Address = warehouse.Address;
            Latitude = warehouse.Latitude;
            Longitude = warehouse.Longitude;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Latitude));
            OnPropertyChanged(nameof(Longitude));
        }

        public void Delete_Click()
        {
            var warehouse = WarehouseAPI.Instance.Delete(Number) as Warehouse;
            
            if (warehouse != null)
            {
                OnDeleted?.Invoke(warehouse.WarehouseId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование склада", WarehouseAPI.Instance, typeof(Warehouse));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            Navigator.Instance.Open(view, "Редактирование склада");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
