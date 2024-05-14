using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class ProductInWarehouse : UserControl
    {
        public ProductInWarehouse(int warehouseId, string title)
        {
            InitializeComponent();

            DataContext = new ProductsInWarehouseViewModel(myData, warehouseId, title);
        }
    }
}
