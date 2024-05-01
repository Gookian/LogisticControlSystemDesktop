using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class PackageViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public int PackageNumber { get; set; }
        public string StateName { get; set; }
        public DateTime BuildDeadline { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public PackageViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var package = (Package)PackageAPI.Instance.Get(Number);

            PackageNumber = package.Number;
            StateName = package.PackageState.Name;
            BuildDeadline = package.BuildDeadline;

            OnPropertyChanged(nameof(PackageNumber));
            OnPropertyChanged(nameof(StateName));
            OnPropertyChanged(nameof(BuildDeadline));
        }

        public void Delete_Click()
        {
            var package = PackageAPI.Instance.Delete(Number) as Package;
            
            if (package != null)
            {
                OnDeleted?.Invoke(package.PackageId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование посылки", PackageAPI.Instance, typeof(Package));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            MainNavigator.Instance.Open(view, "Редактирование посылки");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
