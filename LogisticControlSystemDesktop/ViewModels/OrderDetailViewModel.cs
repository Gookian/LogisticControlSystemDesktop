using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class OrderDetailViewModel : BindableBase
    {
        public int Number { get; set; }
        public string ProductName { get; set; }
        public string Count { get; set; }
        public string Sum { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public OrderDetailViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        public void Delete_Click()
        {
            WarehouseAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование деталей заказа", OrderDetailAPI.Instance, typeof(OrderDetail));

            MainNavigator.Instance.Open(view, "Редактирование деталей заказа");
        }
    }
}
