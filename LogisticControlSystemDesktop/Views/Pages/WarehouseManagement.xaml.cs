using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class WarehouseManagement : UserControl
    {
        public WarehouseManagement()
        {
            InitializeComponent();

            DataContext = new WarehouseManagementViewModel(myData);
        }
    }
}
