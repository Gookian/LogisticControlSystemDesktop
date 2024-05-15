using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.Models.Hubs;
using LogisticControlSystemDesktop.Models.REST.API;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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

        private WarehouseNotificationHub _hub = new WarehouseNotificationHub();
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
                new Parametr { ID = 3, Text = "Адрес", PropertyName = "Address" },
                new Parametr { ID = 4, Text = "Широта", PropertyName = "Latitude" },
                new Parametr { ID = 5, Text = "Долгота", PropertyName = "Longitude" },
            };
            ParametrSelected = Parametrs[0];

            _hub.OnReceivedNotification += hub_OnReceivedNotification;
            _hub.ConnectAsync();

            Load();
        }

        private void hub_OnReceivedNotification(Warehouse entity, UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Add:
                    Create(entity);
                    break;
                case UpdateType.Uppdate:
                    Update(entity);
                    break;
                case UpdateType.Delete:
                    Delete(entity);
                    break;
            }
        }

        private void Create(Warehouse entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _warehouses.Add(viewModel);
            });
        }

        private void Update(Warehouse entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _warehouses.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _warehouses.IndexOf(item);
                _warehouses[index] = viewModel;
            });
        }

        private void Delete(Warehouse entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _warehouses.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _warehouses.Remove(item);
            });
        }

        private void Load()
        {
            _warehouses.Clear();

            var warehouses = WarehouseAPI.Instance.GetAll() as IEnumerable<Warehouse>;
            var viewModels = _converter.Convert(warehouses);

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
