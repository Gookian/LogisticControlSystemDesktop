using LogisticControlSystemDesktop.ViewModels;
using System.Reflection;
using System;
using System.Windows;
using System.Net.Http;

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

            ShellViewModel viewModel = new ShellViewModel(border, navigatePanel);
            DataContext = viewModel;
        }
    }
}
