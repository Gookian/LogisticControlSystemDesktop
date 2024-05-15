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
    public class SortingСenterManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<SortingСenterViewModel> _sortingСenters;
        public ObservableCollection<SortingСenterViewModel> SortingСenters
        {
            get { return _sortingСenters; }
            set
            {
                _sortingСenters = value;

                _itemSourceList = new CollectionViewSource() { Source = SortingСenters };
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

        private SortingСenterNotificationHub _hub = new SortingСenterNotificationHub();
        private SortingСenterConverter _converter = new SortingСenterConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public SortingСenterManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            SortingСenters = new ObservableCollection<SortingСenterViewModel>();
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

        private void hub_OnReceivedNotification(SortingСenter entity, UpdateType type)
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

        private void Create(SortingСenter entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _sortingСenters.Add(viewModel);
            });
        }

        private void Update(SortingСenter entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _sortingСenters.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _sortingСenters.IndexOf(item);
                _sortingСenters[index] = viewModel;
            });
        }

        private void Delete(SortingСenter entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _sortingСenters.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _sortingСenters.Remove(item);
            });
        }

        private void Load()
        {
            _sortingСenters.Clear();

            var SortingСenters = SortingСenterAPI.Instance.GetAll() as IEnumerable<SortingСenter>;
            var viewModels = _converter.Convert(SortingСenters);

            _sortingСenters.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (SortingСenterViewModel)item;
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
