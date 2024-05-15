using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.converters;
using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class ProductInfoViewModel : BindableBase, INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string OverallDemensions { get; set; }
        public string State { get; set; }
        public string Cost { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
        public string Weight { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _productId;

        public ProductInfoViewModel(string title, int productId)
        {
            _productId = productId;

            Title = "Товар: " + title;
            OnPropertyChanged(nameof(Title));

            Load();
        }

        private void Load()
        {
            var product = ProductAPI.Instance.Get(_productId) as Product;
            var productData = ProductDataAPI.Instance.Get(product.ProductDataId) as ProductData;
            var productState = ProductStateAPI.Instance.Get(product.ProductStateId) as ProductState;

            Number = new string('0', 8 - product.ProductId.ToString().Length) + product.ProductId.ToString();
            Article = productData.Article;
            Name = productData.Name;
            OverallDemensions = GetOverallDemensions(productData.Width, productData.Height, productData.Depth, productData.Weight);
            Cost = productData.Cost + " руб";
            Width = productData.Width + " см";
            Height = productData.Height + " см";
            Depth = productData.Depth + " см";
            Weight = productData.Weight + " см";
            State = productState.Name;
        }

        private string GetOverallDemensions(double width, double height, double depth, double weight)
        {
            if (width > 230 || height > 230 || depth > 230 || weight > 25)
            {
                return "Выходит за максимум габаритов";
            }

            if (width > 115 || height > 115 || depth > 115 || (width + height + depth) > 200 || weight > 25)
            {
                return "Сверхкрупногабаритный товар";
            }
            if (width > 50 || height > 50 || depth > 50 || (width + height + depth) > 90 || weight > 5)
            {
                return "Крупногабаритный товар";
            }
            return "Габаритный товар";
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
