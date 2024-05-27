using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.Models.REST.API;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class OrderDetailManagmentViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<OrderDetailViewModel> _orderDetails;
        public ObservableCollection<OrderDetailViewModel> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                _orderDetails = value;

                _itemSourceList = new CollectionViewSource() { Source = OrderDetails };
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

        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ICollectionView _myData;

        private OrderDetailConverter _converter = new OrderDetailConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;
        private int _orderId;

        public OrderDetailManagmentViewModel(DataGrid grid, int orderId, string title)
        {
            _grid = grid;
            _orderId = orderId;

            Title = "Товары на складе: " + title;
            OnPropertyChanged(nameof(Title));

            OrderDetails = new ObservableCollection<OrderDetailViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Данные о товаре", PropertyName = "DataName" },
                new Parametr { ID = 2, Text = "Состояние", PropertyName = "StateName" }
            };
            ParametrSelected = Parametrs[0];

            Load();
        }

        private void Load()
        {
            _orderDetails.Clear();

            var orderDetails = OrderDetailAPI.Instance.GetAll() as IEnumerable<OrderDetail>;
            var orderDetailsById = orderDetails.Where(x => x.OrderId == _orderId);
            var viewModels = _converter.Convert(orderDetailsById);

            _orderDetails.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (ProductViewModel)item;
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
