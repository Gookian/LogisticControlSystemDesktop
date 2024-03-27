using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class TextBoxValidationViewModel : BaseFieldViewModel, INotifyPropertyChanged
    {
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public string Title { get; set; } = "Заголовок";
        public override string Text { get => base.Text; set => base.Text = value; }
        //string Text { get; set; } = "0/10";
        public string Error { get; set; }

        //private string _value;//#FFEE5E5E
        public override string Value { 
            get => base.Value;
            set {
                if (value.Length <= _maxCount)
                {
                    base.Value = value;

                    Text = base.Value.Length + "/" + _maxCount;
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(Text));
                }
            } 
        }
        /*public string Value {
            get { return _value; }
            set {
                if (value.Length <= _maxCount)
                {
                    _value = value;

                    Text = _value.Length + "/" + _maxCount;
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(Text));
                }
            }
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        private int _maxCount = 100;

        public TextBoxValidationViewModel(string name, string title, string value) 
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
