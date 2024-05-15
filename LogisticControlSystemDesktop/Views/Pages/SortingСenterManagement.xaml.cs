using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class SortingСenterManagement : UserControl
    {
        public SortingСenterManagement()
        {
            InitializeComponent();

            DataContext = new SortingСenterManagementViewModel(myData);
        }
    }
}
