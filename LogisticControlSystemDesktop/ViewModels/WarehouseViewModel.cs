using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class WarehouseViewModel : BindableBase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public WarehouseViewModel()
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
            var view = new Edit(Number, "Редактирование склада", WarehouseAPI.Instance, typeof(Warehouse));

            MainNavigator.Instance.Open(view, "Редактирование склада");
        }
    }
}
