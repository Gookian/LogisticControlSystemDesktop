﻿using LogisticControlSystemDesktop.Models;
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
    public class ProductDataManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<ProductDataViewModel> _productDatas;
        public ObservableCollection<ProductDataViewModel> ProductDatas
        {
            get { return _productDatas; }
            set
            {
                _productDatas = value;

                _itemSourceList = new CollectionViewSource() { Source = ProductDatas };
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

        private ProductDataConverter  _converter = new ProductDataConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public ProductDataManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            ProductDatas = new ObservableCollection<ProductDataViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "Number" },
                new Parametr { ID = 2, Text = "Наименование", PropertyName = "Name" },
                new Parametr { ID = 2, Text = "Артикул", PropertyName = "Article" },
                new Parametr { ID = 2, Text = "Цена", PropertyName = "Cost" },
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
            var element = (ProductData)item;
            _productDatas.Add(_converter.Convert(element));
        }

        private void ViewModel_OnDeleted(int id)
        {
            var viewModel = _productDatas.FirstOrDefault(x => x.Number == id);

            if (viewModel != null)
            {
                _productDatas.Remove(viewModel);
            }
        }

        private void UpdateVehicles()
        {
            _productDatas.Clear();

            var productDatas = ProductDataAPI.Instance.GetAll() as IEnumerable<ProductData>;
            var viewModels = _converter.Convert(productDatas);

            foreach (var viewModel in viewModels)
            {
                viewModel.OnDeleted += ViewModel_OnDeleted;
            }

            _productDatas.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (ProductDataViewModel)item;
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