using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class ProductManagement : UserControl
    {
        public ProductManagement()
        {
            InitializeComponent();

            DataContext = new ProductManagementViewModel(myData);
        }
    }
}
