using LogisticControlSystemDesktop.ViewModels;
using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class ProductInfo : UserControl
    {
        public ProductInfo(string title, int productId)
        {
            InitializeComponent();

            DataContext = new ProductInfoViewModel(title, productId);
        }
    }
}
