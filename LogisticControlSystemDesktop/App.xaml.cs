using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticControlSystemDesktop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            new VehicleAPI();
            new WarehouseAPI();
            new FlightAPI();
            new OrderAPI();
            new PackageAPI();
            new ProductAPI();
            new ProductDataAPI();
            new DeliveryAPI();
            new DeliveryPointAPI();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
