using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Mvvm;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public string DisplayName { get; set; } = "Система контроля логистики";

        public ShellViewModel(Border mainPanel)
        {
            ShellNavigator navigator = new ShellNavigator(mainPanel);

            navigator.Open(new Authentication(), "Авторизация");
        }
    }
}