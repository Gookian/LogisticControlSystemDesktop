using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class AuthenticationViewModel : BindableBase
    {
        public DelegateCommand AuthenticationClick { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public AuthenticationViewModel()
        {
            AuthenticationClick = new DelegateCommand(Authentication_Click);
        }

        public void Authentication_Click()
        {
            Guid? token = AuthenticationAPI.Instance.Authentication(Login, Password);

            if (token != null)
            {
                ShellNavigator.Instance.Open(new Main(), "Оснавная");
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
            }
        }
    }
}
