using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class PackageManagement : UserControl
    {
        public PackageManagement()
        {
            InitializeComponent();

            DataContext = new PackageManagementViewModel(myData);
        }
    }
}
