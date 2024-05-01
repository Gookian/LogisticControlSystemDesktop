using LogisticControlSystemDesktop.ViewModels;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();

            MainViewModel viewModel = new MainViewModel(border, navigatePanel);
            DataContext = viewModel;
        }
    }
}
