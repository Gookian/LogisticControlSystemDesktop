using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class ProductDataManagement : UserControl
    {
        public ProductDataManagement()
        {
            InitializeComponent();

            DataContext = new ProductDataManagementViewModel(myData);
        }
    }
}
