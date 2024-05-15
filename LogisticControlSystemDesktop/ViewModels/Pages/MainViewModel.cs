using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels.UserControls;
using LogisticControlSystemDesktop.Views.Pages;
using LogisticControlSystemDesktop.Views.UserControls;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public string DisplayName { get; set; } = "Система контроля логистики";

        public DelegateCommand<string> Open { get; set; }
        public DelegateCommand Exit { get; set; }

        private Guid _target;
        private StackPanel _navigatePanel;
        private Dictionary<Guid, ButtonNavigate> _buttonNavigates = new Dictionary<Guid, ButtonNavigate>();
        private Dictionary<string, Type> _registerScreen = new Dictionary<string, Type>();

        public MainViewModel(Border border, StackPanel navigatePanel)
        {
            _navigatePanel = navigatePanel;

            Open = new DelegateCommand<string>(Open_Click);
            Exit = new DelegateCommand(Exit_Click);

            MainNavigator navigator = new MainNavigator(border);
            navigator.OnOpen += Navigator_OnOpen;
            navigator.OnNavigate += Navigator_OnNavigate;
            navigator.OnClose += Navigator_OnClose;

            navigator.Open(new Home(), "Главная");

            _registerScreen.Add("Пустая страница", typeof(UnimplementedFunctionalityStub));
            _registerScreen.Add("Главная", typeof(Home));
            _registerScreen.Add("Управление транспортными средствами", typeof(VehicleManagement));
            _registerScreen.Add("Управление складами", typeof(WarehouseManagement));
            _registerScreen.Add("Сортировочными центрами", typeof(SortingСenterManagement));
            _registerScreen.Add("Пунктами выдачи товара", typeof(OrderPickUpPointManagement));
            _registerScreen.Add("Управление маршрутами", typeof(FlightManagement));
            _registerScreen.Add("Управление заказами", typeof(OrderManagement));
            _registerScreen.Add("Управление посылками", typeof(PackageManagement));
            _registerScreen.Add("Управление товарами", typeof(ProductManagement));
            _registerScreen.Add("Управление данными товаров", typeof(ProductDataManagement));
            _registerScreen.Add("Управление точками доставки", typeof(DeliveryPointManagement));
            _registerScreen.Add("Vehicle", typeof(Create));
            _registerScreen.Add("Warehouse", typeof(Create));
            _registerScreen.Add("SortingСenter", typeof(Create));
            _registerScreen.Add("OrderPickUpPoint", typeof(Create));
            _registerScreen.Add("DeliveryPoint", typeof(Create));
            _registerScreen.Add("Order", typeof(Create));
            _registerScreen.Add("Product", typeof(Create));
            _registerScreen.Add("ProductData", typeof(Create));
        }

        private void Exit_Click()
        {
            AuthenticationAPI.Instance.RemoveAuthentication();
            ShellViewModel.Instance.Navigator.Open(new Authentication(), "Авторизация");
        }

        public void Open_Click(string title)
        {
            try
            {
                var screen = _registerScreen[title];

                if (screen != typeof(Create))
                {
                    object instance = Activator.CreateInstance(screen);
                    MainNavigator.Instance.Open((UserControl)instance, title);
                }
                else
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    BaseEntityAPI api = assembly.CreateInstance($"LogisticControlSystemDesktop.Models.REST.API.{title}API") as BaseEntityAPI;
                    Type typeEntyty = Type.GetType($"LogisticControlSystemDesktop.Models.{title}");
                    Type typeView = Type.GetType("LogisticControlSystemDesktop.Views.Pages.Create");

                    string screenName = "";
                    switch (title)
                    {
                        case "Vehicle":
                            screenName = "Cоздание транспортного средства";
                            break;
                        case "Warehouse":
                            screenName = "Cоздание склада";
                            break;
                        case "SortingСenter":
                            screenName = "Cоздание сортировочного центра";
                            break;
                        case "OrderPickUpPoint":
                            screenName = "Cоздание пункта выдачи заказов";
                            break;
                        case "DeliveryPoint":
                            screenName = "Cоздание точки доставки";
                            break;
                        case "Order":
                            screenName = "Cоздание заказа";
                            break;
                        case "Product":
                            screenName = "Cоздание товара";
                            break;
                        case "ProductData":
                            screenName = "Cоздание данных товара";
                            break;
                        default:
                            break;
                    }

                    object instance = Activator.CreateInstance(typeView, new object[] { screenName, api, typeEntyty });
                    MainNavigator.Instance.Open((UserControl)instance, screenName);
                }
            }
            catch (Exception) { }
        }

        private void Navigator_OnNavigate(Guid id)
        {
            _target = id;

            SetAccentColor();
        }

        private void Navigator_OnOpen(Guid id, string title)
        {
            _target = id;

            var newButton = new ButtonNavigate(id, title);

            _buttonNavigates.Add(id, newButton);
            _navigatePanel.Children.Add(newButton);

            SetAccentColor();
        }

        private void Navigator_OnClose(Guid id)
        {
            var button = _buttonNavigates[id];

            _navigatePanel.Children.Remove(button);
            _buttonNavigates.Remove(id);

            var item = _buttonNavigates.LastOrDefault();

            _target = item.Key;

            MainNavigator.Instance.Navigate(_target);

            SetAccentColor();
        }

        private void SetAccentColor()
        {
            foreach (var button in _buttonNavigates)
            {
                var viewModel = (button.Value.DataContext as ButtonNavigateViewModel);

                if (button.Key == _target)
                {
                    viewModel.Color = viewModel.AccentColor;
                }
                else
                {
                    viewModel.Color = viewModel.MainColor;
                }
            }
        }
    }
}