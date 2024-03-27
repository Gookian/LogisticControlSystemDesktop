using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using LogisticControlSystemDesktop.Views.Pages;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ProductViewModel : BindableBase, INotifyPropertyChanged
    {
        public int Number { get; set; }
        public string StateName { get; set; }
        public string DataName { get; set; }
        public int ProductDataId { get; set; }

        public DelegateCommand DeleteClick { get; set; }
        public DelegateCommand EditClick { get; set; }

        public delegate void DeleteHandler(int id);
        public event DeleteHandler OnDeleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductViewModel()
        {
            DeleteClick = new DelegateCommand(Delete_Click);
            EditClick = new DelegateCommand(Edit_Click);
        }

        private void ViewModel_OnSaved()
        {
            var product = (Product)ProductAPI.Instance.Get(Number);

            StateName = product.ProductState.Name;
            DataName = product.ProductData.Name;
            ProductDataId = product.ProductDataId;

            OnPropertyChanged(nameof(StateName));
            OnPropertyChanged(nameof(DataName));
            OnPropertyChanged(nameof(ProductDataId));
        }

        public void Delete_Click()
        {
            var product = ProductAPI.Instance.Delete(Number) as Product;
            
            if (product != null)
            {
                OnDeleted?.Invoke(product.ProductId);
            }
        }

        public void Edit_Click()
        {
            var view = new Edit(Number, "Редактирование товара", ProductAPI.Instance, typeof(Product));
            var viewModel = view.DataContext as EditViewModel;

            viewModel.OnSaved += ViewModel_OnSaved;

            Navigator.Instance.Open(view, "Редактирование товара");
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
