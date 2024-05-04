using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class TextBoxValidationViewModel : BaseFieldViewModel, INotifyPropertyChanged
    {
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public string Title { get; set; } = "Заголовок";
        public override string Text { get => base.Text; set => base.Text = value; }
        public string Hint { get; set; } = "Введите текст";
        public string Error { get; set; }

        public override string Value { 
            get => base.Value;
            set {
                if (value.Length <= _maxCount)
                {
                    base.Value = value;

                    Text = base.Value.Length + "/" + _maxCount;
                    Error = "";
                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(Text));
                    OnPropertyChanged(nameof(Error));
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Regex _pattern = new Regex(@"");
        private int _maxCount;
        private int _minCount;

        public TextBoxValidationViewModel(string name, string title, string hint, string value, int min, int max, string pattern) 
        {
            FieldName = name;
            Title = title;
            Hint = hint;
            Value = value;

            _pattern = new Regex(pattern);
            _maxCount = max;
            _minCount = min;

            Text = Value.Length + "/" + _maxCount;
            OnPropertyChanged(nameof(Text));
        }

        public bool Validate()
        {
            if (Value.Length >= _minCount && Value.Length <= _maxCount)
            {
                if(_pattern.IsMatch(Value))
                {
                    Error = "";
                    OnPropertyChanged(nameof(Error));
                    return true;
                }

                Error = "Неверный формат ввода";
                OnPropertyChanged(nameof(Error));
                return false;
            }

            Error = "Длина должна быть от " + _minCount + " до " + _maxCount;
            OnPropertyChanged(nameof(Error));
            return false;
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
