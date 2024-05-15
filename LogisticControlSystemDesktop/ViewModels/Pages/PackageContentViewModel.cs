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
    public class PackageContentViewModel : BindableBase, INotifyPropertyChanged
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

        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ICollectionView _myData;

        private ProductConverter _converter = new ProductConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;
        private int _packageId;

        public PackageContentViewModel(DataGrid grid, int packageId, string title)
        {
            _grid = grid;
            _packageId = packageId;

            Title = "Товары прикрепленные к посылке: " + title;
            OnPropertyChanged(nameof(Title));

            Products = new ObservableCollection<ProductViewModel>();
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
            _products.Clear();

            var packageContent = PackageContentAPI.Instance.GetAll() as IEnumerable<PackageContent>;
            var packageContentIds = packageContent.Where(x => x.PackageId == _packageId).Select(x => x.ProductId);
            var products = ProductAPI.Instance.GetAll() as IEnumerable<Product>;
            var productsById = products.Where(x => packageContentIds.Contains(x.ProductId));
            var viewModels = _converter.Convert(productsById);

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
