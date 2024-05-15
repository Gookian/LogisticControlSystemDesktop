using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Mvvm;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        static public ShellViewModel Instance { get; set; }
        public string DisplayName { get; set; } = "Система контроля логистики";
        public ShellNavigator Navigator { get; set; }


        public ShellViewModel(Border mainPanel)
        {
            Navigator = new ShellNavigator(mainPanel);

            Navigator.Open(new Authentication(), "Авторизация");

            Instance = this;
        }
    }
}