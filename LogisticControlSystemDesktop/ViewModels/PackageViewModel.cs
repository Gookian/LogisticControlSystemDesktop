using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class PackageViewModel : BindableBase
    {
        public int Number { get; set; }
        public int PackageNumber { get; set; }
        public string StateName { get; set; }
        public DateTime BuildDeadline { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public PackageViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        public void Delete_Click()
        {
            PackageAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование посылки", PackageAPI.Instance, typeof(Package));

            MainNavigator.Instance.Open(view, "Редактирование посылки");
        }
    }
}
