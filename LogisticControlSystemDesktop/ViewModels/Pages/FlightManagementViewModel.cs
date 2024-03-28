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

            UpdateVehicles();
        }

        public void SignOnCreated(CreateViewModel viewModel)
        {
            viewModel.OnCreated += ViewModel_OnCreated;
        }

        private void ViewModel_OnCreated(object item)
        {
            var element = (Flight)item;
            _flights.Add(_converter.Convert(element));
        }

        private void ViewModel_OnDeleted(int id)
        {
            var viewModel = _flights.FirstOrDefault(x => x.Number == id);

            if (viewModel != null)
            {
                _flights.Remove(viewModel);
            }
        }

        private void UpdateVehicles()
        {
            _flights.Clear();

            var flights = FlightAPI.Instance.GetAll() as IEnumerable<Flight>;
            var viewModels = _converter.Convert(flights);

            foreach (var viewModel in viewModels)
            {
                viewModel.OnDeleted += ViewModel_OnDeleted;
            }

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