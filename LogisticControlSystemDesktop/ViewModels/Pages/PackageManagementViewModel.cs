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
    public class PackageManagementViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<PackageViewModel> _packages;
        public ObservableCollection<PackageViewModel> Packages
        {
            get { return _packages; }
            set
            {
                _packages = value;

                _itemSourceList = new CollectionViewSource() { Source = Packages };
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

        private PackageNotificationHub _hub = new PackageNotificationHub();
        private PackageConverter _converter = new PackageConverter();
        private CollectionViewSource _itemSourceList;
        private DataGrid _grid;

        public PackageManagementViewModel(DataGrid grid)
        {
            _grid = grid;

            Packages = new ObservableCollection<PackageViewModel>();
            Parametrs = new ObservableCollection<Parametr>
            {
                new Parametr { ID = 1, Text = "#", PropertyName = "PackageNumber" },
                new Parametr { ID = 2, Text = "Состояние", PropertyName = "StateName" },
                new Parametr { ID = 2, Text = "Дедлайн сборки", PropertyName = "BuildDeadline" }
            };
            ParametrSelected = Parametrs[0];

            _hub.OnReceivedNotification += hub_OnReceivedNotification;
            _hub.ConnectAsync();

            Load();
        }

        private void hub_OnReceivedNotification(Package entity, UpdateType type)
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

        private void Create(Package entity)
        {
            var viewModel = _converter.Convert(entity);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _packages.Add(viewModel);
            });
        }

        private void Update(Package entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _packages.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                int index = _packages.IndexOf(item);
                _packages[index] = viewModel;
            });
        }

        private void Delete(Package entity)
        {
            var viewModel = _converter.Convert(entity);
            var item = _packages.FirstOrDefault(x => x.Number == viewModel.Number);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _packages.Remove(item);
            });
        }

        private void Load()
        {
            _packages.Clear();

            var packages = PackageAPI.Instance.GetAll() as IEnumerable<Package>;
            var viewModels = _converter.Convert(packages);

            _packages.AddRange(viewModels);
        }

        private bool FilterData(object item)
        {
            var value = (PackageViewModel)item;
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
