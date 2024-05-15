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
    public class OrderManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<OrderViewModel> _orders;
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;

                _itemSourceList = new CollectionViewSource() { Source = Orders };
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

        private OrderNotificationHub _hub = new OrderNotificationHub();
        private OrderConverter _converter = new OrderConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public OrderManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Orders = new ObservableCollection<OrderViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Имя", PropertyName = "FirstName" },
                new Parametr { ID = 2, Text = "Фамилия", PropertyName = "MiddleName" },
                new Parametr { ID = 2, Text = "Отчество", PropertyName = "Surname" },
                new Parametr { ID = 2, Text = "Адрес", PropertyName = "Address" },
                new Parametr { ID = 2, Text = "Дата доставки", PropertyName = "DeliveryDateTime" },
            };
            ParametrSelected = Parametrs[0];

            _hub.OnReceivedNotification += hub_OnReceivedNotification;
            _hub.ConnectAsync();

            Load();
        }

        private void hub_OnReceivedNotification(Order entity, UpdateType type)
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

        private void Create(Order entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _orders.Add(viewModel);
            });
        }

        private void Update(Order entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _orders.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _orders.IndexOf(item);
                _orders[index] = viewModel;
            });
        }

        private void Delete(Order entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _orders.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _orders.Remove(item);
            });
        }

        private void Load()
        {
            _orders.Clear();

            var orders = OrderAPI.Instance.GetAll() as IEnumerable<Order>;
            var viewModels = _converter.Convert(orders);

            _orders.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (OrderViewModel)item;
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
