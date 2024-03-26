using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.Models
{
    public class VehicleViewModel : BindableBase, INotifyPropertyChanged
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

        public delegate void DeleteHandler(int id);
        public event DeleteHandler VehicleDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public VehicleViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var vehicle = (Vehicle)VehicleAPI.Instance.Get(Number);

            Brand = vehicle.Brand;
            RegistrationNumber = vehicle.RegistrationNumber;
            Capacity = vehicle.Capacity + " м.кв.";
            LoadCapacity = vehicle.LoadCapacity + " тон";

            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(RegistrationNumber));
            OnPropertyChanged(nameof(Capacity));
            OnPropertyChanged(nameof(LoadCapacity));
        }

        public void Delete_Click()
        {
            var vehicle = VehicleAPI.Instance.Delete(Number) as Vehicle;
            
            if (vehicle != null)
            {
                VehicleDeleted?.Invoke(vehicle.VehicleId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование транспортного средства", VehicleAPI.Instance, typeof(Vehicle));
            var viewModel = view.DataContext as VehicleEditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            Navigator.Instance.Open(view, "Редактирование ТС");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
