using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class ComboBoxValidationViewModel : BaseFieldViewModel, INotifyPropertyChanged
    {
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public string Title { get; set; } = "Заголовок";
        public string Text { get; set; } = "0/10";
        public string Error { get; set; }

        public override string Value
        {
            get => base.Value;
            set
            {
                if (value.Length <= _maxCount)
                {
                    base.Value = value;

                    Text = base.Value.Length + "/" + _maxCount;
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _maxCount = 100;

        public ComboBoxValidationViewModel(string name, string title, string value) 
        {
            FieldName = name;
            Title = title;
            Value = value;
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
