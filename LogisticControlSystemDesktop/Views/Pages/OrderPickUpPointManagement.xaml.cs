using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class OrderPickUpPointManagement : UserControl
    {
        public OrderPickUpPointManagement()
        {
            InitializeComponent();

            DataContext = new OrderPickUpPointManagementViewModel(myData);
        }
    }
}
