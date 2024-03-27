using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class FlightManagement : UserControl
    {
        public FlightManagement()
        {
            InitializeComponent();

            DataContext = new FlightManagementViewModel(myData);
        }
    }
}
