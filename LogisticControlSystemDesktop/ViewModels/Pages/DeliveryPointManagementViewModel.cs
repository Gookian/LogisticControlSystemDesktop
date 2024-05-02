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
    public class DeliveryPointManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<DeliveryPointViewModel> _deliveryPoints;
        public ObservableCollection<DeliveryPointViewModel> DeliveryPoints
        {
            get { return _deliveryPoints; }
            set
            {
                _deliveryPoints = value;

                _itemSourceList = new CollectionViewSource() { Source = DeliveryPoints };
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

        private DeliveryPointNotificationHub _hub = new DeliveryPointNotificationHub();
        private DeliveryPointConverter _converter = new DeliveryPointConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public DeliveryPointManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            DeliveryPoints = new ObservableCollection<DeliveryPointViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Наименование", PropertyName = "Name" },
                new Parametr { ID = 2, Text = "Адрес", PropertyName = "Address" },
                new Parametr { ID = 2, Text = "Широта", PropertyName = "Latitude" },
                new Parametr { ID = 2, Text = "Долгота", PropertyName = "Address" },
            };
            ParametrSelected = Parametrs[0];

            _hub.OnReceivedNotification += hub_OnReceivedNotification;
            _hub.ConnectAsync();

            Load();
        }

        private void hub_OnReceivedNotification(DeliveryPoint entity, UpdateType type)
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

        private void Create(DeliveryPoint entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _deliveryPoints.Add(viewModel);
            });
        }

        private void Update(DeliveryPoint entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _deliveryPoints.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _deliveryPoints.IndexOf(item);
                _deliveryPoints[index] = viewModel;
            });
        }

        private void Delete(DeliveryPoint entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _deliveryPoints.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _deliveryPoints.Remove(item);
            });
        }

        private void Load()
        {
            _deliveryPoints.Clear();

            var deliveryPoints = DeliveryPointAPI.Instance.GetAll() as IEnumerable<DeliveryPoint>;
            var viewModels = _converter.Convert(deliveryPoints);

            _deliveryPoints.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (DeliveryPointViewModel)item;
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
