using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        public int Number { get; set; }
        public string StateName { get; set; }
        public string DataName { get; set; }
        public int ProductDataId { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }
        public DelegateCommand OpenProductClick { get; set; }

        public ProductViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
            OpenProductClick = new DelegateCommand(OpenProduct_Click);
        }

        private void OpenProduct_Click()
        {
            MainNavigator.Instance.Open(new ProductInfo(DataName, Number), DataName);
        }

        public void Delete_Click()
        {
            ProductAPI.Instance.Delete(Number);
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование товара", ProductAPI.Instance, typeof(Product));

            MainNavigator.Instance.Open(view, "Редактирование товара");
        }
    }
}
