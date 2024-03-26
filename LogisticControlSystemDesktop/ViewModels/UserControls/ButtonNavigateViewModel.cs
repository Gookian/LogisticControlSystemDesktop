using LogisticControlSystemDesktop.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class ButtonNavigateViewModel : BindableBase, INotifyPropertyChanged
    {
        public string Title { get; set; } = "Главная";

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        public DelegateCommand NavigateTo { get; set; }
        public DelegateCommand Close { get; set; }

        public SolidColorBrush AccentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39AFEA"));
        public SolidColorBrush MainColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5e69ee"));

        public event PropertyChangedEventHandler PropertyChanged;

        private Guid _id;
        private SolidColorBrush _color;

        public ButtonNavigateViewModel(Guid id, string title)
        {
            Color = MainColor;
            Title = title;

            _id = id;

            NavigateTo = new DelegateCommand(NavigateTo_Click);
            Close = new DelegateCommand(Close_Click);
        }

        public void NavigateTo_Click()
        {
            Color = AccentColor;
            Navigator.Instance.Navigate(_id);
        }

        public void Close_Click()
        {
            Navigator.Instance.Close(_id);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
