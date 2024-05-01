using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.ViewModels.UserControls;
using LogisticControlSystemDesktop.Views.Pages;
using LogisticControlSystemDesktop.Views.UserControls;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public string DisplayName { get; set; } = "Система контроля логистики";

        public ShellViewModel(Border mainPanel)
        {
            ShellNavigator navigator = new ShellNavigator(mainPanel, new Authentication());

            navigator.Open(new Authentication(), "Авторизация");
        }
    }
}