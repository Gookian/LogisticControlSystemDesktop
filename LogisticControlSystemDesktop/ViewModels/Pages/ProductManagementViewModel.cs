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
    public class ProductManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<ProductViewModel> _products;
        public ObservableCollection<ProductViewModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;

                _itemSourceList = new CollectionViewSource() { Source = Products };
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

        private ProductConverter _converter = new ProductConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public ProductManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Products = new ObservableCollection<ProductViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Данные о товаре", PropertyName = "DataName" },
                new Parametr { ID = 2, Text = "Состояние", PropertyName = "StateName" }
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
            var element = (Product)item;
            _products.Add(_converter.Convert(element));
        }

        private void ViewModel_OnDeleted(int id)
        {
            var viewModel = _products.FirstOrDefault(x => x.Number == id);

            if (viewModel != null)
            {
                _products.Remove(viewModel);
            }
        }

        private void UpdateVehicles()
        {
            _products.Clear();

            var products = ProductAPI.Instance.GetAll() as IEnumerable<Product>;
            var viewModels = _converter.Convert(products);

            foreach (var viewModel in viewModels)
            {
                viewModel.OnDeleted += ViewModel_OnDeleted;
            }

            _products.AddRange(viewModels);
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
