using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class OrderManagement : UserControl
    {
        public OrderManagement()
        {
            InitializeComponent();

            DataContext = new OrderManagementViewModel(myData);
        }
    }
}
