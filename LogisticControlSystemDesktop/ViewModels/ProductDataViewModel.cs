using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.Navigators;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ProductDataViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public int Cost { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductDataViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var productData = (ProductData)ProductDataAPI.Instance.Get(Number);

            Name = productData.Name;
            Article = productData.Article;
            Cost = productData.Cost;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Article));
            OnPropertyChanged(nameof(Cost));
        }

        public void Delete_Click()
        {
            var productData = ProductDataAPI.Instance.Delete(Number) as ProductData;
            
            if (productData != null)
            {
                OnDeleted?.Invoke(productData.ProductDataId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование данных товара", ProductDataAPI.Instance, typeof(ProductData));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            MainNavigator.Instance.Open(view, "Редактирование данных товара");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
