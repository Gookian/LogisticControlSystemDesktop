using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class OrderDetailManagment : UserControl
    {
        public OrderDetailManagment(int orderId, string title)
        {
            InitializeComponent();

            DataContext = new OrderDetailManagmentViewModel(myData, orderId, title);
        }
    }
}
