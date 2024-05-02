using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ProductDataViewModel : BindableBase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public int Cost { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public ProductDataViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        public void Delete_Click()
        {
            ProductDataAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование данных товара", ProductDataAPI.Instance, typeof(ProductData));

            MainNavigator.Instance.Open(view, "Редактирование данных товара");
        }
    }
}
