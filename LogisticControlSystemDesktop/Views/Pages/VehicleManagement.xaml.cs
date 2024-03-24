using LogisticControlSystemDesktop.ViewModels;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class VehicleManagement : UserControl
    {
        public VehicleManagement()
        {
            InitializeComponent();

            DataContext = new VehicleManagementViewModel();
        }
    }
}
