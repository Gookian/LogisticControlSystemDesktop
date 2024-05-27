using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class ComboBoxValidationViewModel : BaseFieldViewModel, INotifyPropertyChanged
    {
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public string Title { get; set; } = "Заголовок";
        public string Hint { get; set; } = "Введите текст";
        public override ObservableCollection<IdTargetValueItem> Parametrs
        {
            get => base.Parametrs;
            set
            {
                base.Parametrs = value;
                OnPropertyChanged(nameof(Parametrs));
            }
        }
        public override IdTargetValueItem ParametrSelected
        {
            get => base.ParametrSelected;
            set
            {
                base.ParametrSelected = value;
                Value = value.Value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(ParametrSelected));
            }
        }
        public string Error { get; set; }
        public override string Value { get => base.Value; set => base.Value = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ComboBoxValidationViewModel(string name, string title, string hint, int id) 
        {
            FieldName = name;
            Title = title;
            Hint = hint;

            Parametrs = new ObservableCollection<IdTargetValueItem>();
            Parametrs.AddRange(GetParametrs());
        }

        public override void SetSelected(int id)
        {
            var parametr = Parametrs.FirstOrDefault(x => x.Id == id);

            if (parametr != null)
            {
                ParametrSelected = parametr;
            }
            else
            {
                if (Parametrs.Count >= 1)
                {
                    ParametrSelected = Parametrs.First();
                }
            }
        }

        private ObservableCollection<IdTargetValueItem> GetParametrs()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            BaseEntityAPI api = assembly.CreateInstance($"LogisticControlSystemDesktop.Models.REST.API.{FieldName.Substring(0, FieldName.Length - 2)}API") as BaseEntityAPI;

            var items = api.GetIdTargetValues() as IEnumerable<object>;

            var parametrs = new ObservableCollection<IdTargetValueItem>();

            foreach (var item in items)
            {
                int id = 0;
                string value = "";

                int i = 0;
                foreach (var property in item.GetType().GetProperties())
                {
                    if (i == 0)
                    {
                        id = (int)property.GetValue(item);
                    }
                    if (i == 1)
                    {
                        value = property.GetValue(item).ToString();
                    }
                    i++;
                }

                parametrs.Add(new IdTargetValueItem()
                {
                    Id = id,
                    Value = value
                });
            }

            return parametrs;
        }

        public bool Validate()
        {
            if (Value.Length != 0)
            {
                Error = "";
                OnPropertyChanged(nameof(Error));
                return true;
            }

            Error = "Ничего не выбрано";
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
