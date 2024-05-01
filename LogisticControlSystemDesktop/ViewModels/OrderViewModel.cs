using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class OrderViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime DeliveryDateTime { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var order = (Order)OrderAPI.Instance.Get(Number);

            FirstName = order.FirstName;
            MiddleName = order.MiddleName;
            Surname = order.Surname;
            Address = order.Address;
            DeliveryDateTime = order.DeliveryDateTime;

            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(DeliveryDateTime));
        }

        public void Delete_Click()
        {
            var order = OrderAPI.Instance.Delete(Number) as Order;
            
            if (order != null)
            {
                OnDeleted?.Invoke(order.OrderId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование заказа", OrderAPI.Instance, typeof(Order));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            MainNavigator.Instance.Open(view, "Редактирование заказа");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
