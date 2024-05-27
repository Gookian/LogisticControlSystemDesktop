using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class OrderViewModel : BindableBase
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string DeliveryDateTime { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }
        public DelegateCommand OpenOrderDetailClick { get; set; }

        public OrderViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
            OpenOrderDetailClick = new DelegateCommand(OpenOrderDetail_Click);
        }

        private void OpenOrderDetail_Click()
        {
            var view = new OrderDetailManagment(Number, $"Детали заказа для: {MiddleName} {FirstName} {Surname}");

            MainNavigator.Instance.Open(view, "Детали заказа");
        }

        public void Delete_Click()
        {
            OrderAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование заказа", OrderAPI.Instance, typeof(Order));

            MainNavigator.Instance.Open(view, "Редактирование заказа");
        }
    }
}
