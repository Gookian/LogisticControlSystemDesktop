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
    public class OrderPickUpPointManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<OrderPickUpPointViewModel> _orderPickUpPoints;
        public ObservableCollection<OrderPickUpPointViewModel> OrderPickUpPoints
        {
            get { return _orderPickUpPoints; }
            set
            {
                _orderPickUpPoints = value;

                _itemSourceList = new CollectionViewSource() { Source = OrderPickUpPoints };
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

        private OrderPickUpPointNotificationHub _hub = new OrderPickUpPointNotificationHub();
        private OrderPickUpPointConverter _converter = new OrderPickUpPointConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public OrderPickUpPointManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            OrderPickUpPoints = new ObservableCollection<OrderPickUpPointViewModel>();
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

        private void hub_OnReceivedNotification(OrderPickUpPoint entity, UpdateType type)
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

        private void Create(OrderPickUpPoint entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _orderPickUpPoints.Add(viewModel);
            });
        }

        private void Update(OrderPickUpPoint entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _orderPickUpPoints.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _orderPickUpPoints.IndexOf(item);
                _orderPickUpPoints[index] = viewModel;
            });
        }

        private void Delete(OrderPickUpPoint entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _orderPickUpPoints.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _orderPickUpPoints.Remove(item);
            });
        }

        private void Load()
        {
            _orderPickUpPoints.Clear();

            var OrderPickUpPoints = OrderPickUpPointAPI.Instance.GetAll() as IEnumerable<OrderPickUpPoint>;
            var viewModels = _converter.Convert(OrderPickUpPoints);

            _orderPickUpPoints.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (OrderPickUpPointViewModel)item;
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
