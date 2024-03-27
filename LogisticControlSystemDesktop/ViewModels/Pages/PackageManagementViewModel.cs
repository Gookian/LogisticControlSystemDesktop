using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class PackageManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<PackageViewModel> _packages;
        public ObservableCollection<PackageViewModel> Packages
        {
            get { return _packages; }
            set
            {
                _packages = value;

                _itemSourceList = new CollectionViewSource() { Source = Packages };
                _myData = _itemSourceList.View;
            }
        }

        private ObservableCollection<Parametr> _parametrs;
        public ObservableCollection<Parametr> Parametrs
        {
            get { return _parametrs; }
            set
            {
                _parametrs = value;
                OnPropertyChanged(nameof(Parametrs));
            }
        }

        private Parametr _parametrSelected;
        public Parametr ParametrSelected
        {
            get { return _parametrSelected; }
            set
            {
                _parametrSelected = value;
                OnPropertyChanged(nameof(ParametrSelected));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                _myData.Filter = FilterData;
                _grid.ItemsSource = _myData;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ICollectionView _myData;

        private PackageConverter _converter = new PackageConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public PackageManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Packages = new ObservableCollection<PackageViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "PackageNumber" },
                new Parametr { ID = 2, Text = "Состояние", PropertyName = "StateName" },
                new Parametr { ID = 2, Text = "Дедлайн сборки", PropertyName = "BuildDeadline" }
            };
            ParametrSelected = Parametrs[0];

            UpdateVehicles();
        }

        public void SignOnCreated(CreateViewModel viewModel)
        {
            viewModel.OnCreated += ViewModel_OnCreated;
        }

        private void ViewModel_OnCreated(object item)
        {
            var element = (Package)item;
            _packages.Add(_converter.Convert(element));
        }

        private void ViewModel_OnDeleted(int id)
        {
            var viewModel = _packages.FirstOrDefault(x => x.Number == id);

            if (viewModel != null)
            {
                _packages.Remove(viewModel);
            }
        }

        private void UpdateVehicles()
        {
            _packages.Clear();

            var packages = PackageAPI.Instance.GetAll() as IEnumerable<Package>;
            var viewModels = _converter.Convert(packages);

            foreach (var viewModel in viewModels)
            {
                viewModel.OnDeleted += ViewModel_OnDeleted;
            }

            _packages.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (PackageViewModel)item;
            if (value == null)
                return false;

            var valueParametr = item.GetType().GetProperty(ParametrSelected.PropertyName).GetValue(item, null);

            return valueParametr.ToString().ToLower().StartsWith(_searchText.ToLower());
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
