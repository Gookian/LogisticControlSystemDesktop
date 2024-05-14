using LogisticControlSystemDesktop.ViewModels.Pages;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleManagement.xaml
    /// </summary>
    public partial class PackageContentView : UserControl
    {
        public PackageContentView(int warehouseId, string title)
        {
            InitializeComponent();

            DataContext = new PackageContentViewModel(myData, warehouseId, title);
        }
    }
}
