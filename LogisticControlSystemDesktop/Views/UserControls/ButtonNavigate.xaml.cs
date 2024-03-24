using LogisticControlSystemDesktop.ViewModels;
using System;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ButtonNavigate.xaml
    /// </summary>
    public partial class ButtonNavigate : UserControl
    {
        public ButtonNavigate(Guid id, string title)
        {
            InitializeComponent();

            ButtonNavigateViewModel viewModel = new ButtonNavigateViewModel(id, title);
            DataContext = viewModel;
        }
    }
}
