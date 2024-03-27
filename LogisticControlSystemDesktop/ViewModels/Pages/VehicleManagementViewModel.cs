using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class VehicleManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<VehicleViewModel> _vihecles;
        public ObservableCollection<VehicleViewModel> Vihecles
        {
            get { return _vihecles; }
            set
            {
                _vihecles = value;

                _itemSourceList = new CollectionViewSource() { Source = Vihecles };
                _myData = _itemSourceList.View;
                OnPropertyChanged(nameof(Vihecles));
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

        private VehicleConverter _converter = new VehicleConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public VehicleManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Vihecles = new ObservableCollection<VehicleViewModel>();
            Parametrs = new ObservableCollection<Parametr>();

            Parametrs.Add(new Parametr { ID = 1, Text = "#", PropertyName = "Number" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Владелец", PropertyName = "Name" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Тип", PropertyName = "Type" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Марка", PropertyName = "Brand" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Регестрационны номер", PropertyName = "RegistrationNumber" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Вместимость", PropertyName = "Capacity" });
            Parametrs.Add(new Parametr { ID = 2, Text = "Грузоподъемность", PropertyName = "LoadCapacity" });
            ParametrSelected = Parametrs[0];

            UpdateVehicles();
        }

        public void SignOnCreated(CreateViewModel viewModel)
        {
            viewModel.OnCreated += ViewModel_OnCreated;
        }

        private void ViewModel_OnCreated(object item)
        {
            var element = (Vehicle)item;
            _vihecles.Add(_converter.Convert(element));
        }

        private void VehicleData_VehicleDeleted(int id)
        {
            var vihecleViewModel = _vihecles.FirstOrDefault(x => x.Number == id);

            if (vihecleViewModel != null)
            {
                _vihecles.Remove(vihecleViewModel);
            }
        }

        private void UpdateVehicles()
        {
            _vihecles.Clear();

            var vehicles = VehicleAPI.Instance.GetAll() as IEnumerable<Vehicle>;
            var vehicleDatas = _converter.Convert(vehicles);

            foreach (var vehicleData in vehicleDatas)
            {
                vehicleData.VehicleDeleted += VehicleData_VehicleDeleted;
            }

            _vihecles.AddRange(vehicleDatas);
        }

        private bool FilterData(object item)
        {
            var value = (VehicleViewModel)item;
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
