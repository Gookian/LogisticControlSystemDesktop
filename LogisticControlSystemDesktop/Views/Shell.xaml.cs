using LogisticControlSystemDesktop.ViewModels;
using System.Windows;

namespace LogisticControlSystemDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            ShellViewModel viewModel = new ShellViewModel(mainPanel);
            DataContext = viewModel;
        }
    }
}
