using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class DeliveryPointManagement : UserControl
    {
        public DeliveryPointManagement()
        {
            InitializeComponent();

            DataContext = new DeliveryPointManagementViewModel(myData);
        }
    }
}
