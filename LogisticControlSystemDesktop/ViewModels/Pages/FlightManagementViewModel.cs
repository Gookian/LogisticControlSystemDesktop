using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.Models.Hubs;
using LogisticControlSystemDesktop.REST.API;
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
    public class FlightManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<FlightViewModel> _flights;
        public ObservableCollection<FlightViewModel> Flights
        {
            get { return _flights; }
            set
            {
                _flights = value;

                _itemSourceList = new CollectionViewSource() { Source = Flights };
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

        private FlightNotificationHub _hub = new FlightNotificationHub();
        private FlightConverter _converter = new FlightConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public FlightManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Flights = new ObservableCollection<FlightViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "FlightNumber" },
                new Parametr { ID = 2, Text = "Транспортное средство", PropertyName = "RegistrationNumber" },
            };
            ParametrSelected = Parametrs[0];

            _hub.OnReceivedNotification += hub_OnReceivedNotification;
            _hub.ConnectAsync();

            Load();
        }

        private void hub_OnReceivedNotification(Flight entity, UpdateType type)
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

        private void Create(Flight entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _flights.Add(viewModel);
            });
        }

        private void Update(Flight entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _flights.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _flights.IndexOf(item);
                _flights[index] = viewModel;
            });
        }

        private void Delete(Flight entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _flights.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _flights.Remove(item);
            });
        }

        private void Load()
        {
            _flights.Clear();

            var flights = FlightAPI.Instance.GetAll() as IEnumerable<Flight>;
            var viewModels = _converter.Convert(flights);

            _flights.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (FlightViewModel)item;
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
