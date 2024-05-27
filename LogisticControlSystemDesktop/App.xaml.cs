using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
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
            new AuthenticationAPI();
            new VehicleAPI();
            new WarehouseAPI();
            new FlightAPI();
            new OrderAPI();
            new OrderDetailAPI();
            new OrderPickUpPointAPI();
            new PackageAPI();
            new PackageStateAPI();
            new PackageContentAPI();
            new ProductAPI();
            new ProductDataAPI();
            new ProductInWarehouseAPI();
            new ProductStateAPI();
            new SortingСenterAPI();
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
