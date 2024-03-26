using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class WarehouseManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<WarehouseViewModel> _warehouses;
        public ObservableCollection<WarehouseViewModel> Warehouses
        {
            get { return _warehouses; }
            set
            {
                _warehouses = value;

                _itemSourceList = new CollectionViewSource() { Source = Warehouses };
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

        private WarehouseConverter _converter = new WarehouseConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public WarehouseManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Warehouses = new ObservableCollection<WarehouseViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Наименование", PropertyName = "Name" },
                new Parametr { ID = 2, Text = "Адрес", PropertyName = "Address" }
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
            var element = (Warehouse)item;
            _warehouses.Add(_converter.Convert(element));
        }

        private void ViewModel_OnDeleted(int id)
        {
            var viewModel = _warehouses.FirstOrDefault(x => x.Number == id);

            if (viewModel != null)
            {
                _warehouses.Remove(viewModel);
            }
        }

        private void UpdateVehicles()
        {
            _warehouses.Clear();

            var warehouses = WarehouseAPI.Instance.GetAll() as IEnumerable<Warehouse>;
            var viewModels = _converter.Convert(warehouses);

            foreach (var viewModel in viewModels)
            {
                viewModel.OnDeleted += ViewModel_OnDeleted;
            }

            _warehouses.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (WarehouseViewModel)item;
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
