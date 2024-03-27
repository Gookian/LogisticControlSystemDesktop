using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.REST.API;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public class ComboBoxValidationViewModel : BaseFieldViewModel, INotifyPropertyChanged
    {
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public string Title { get; set; } = "Заголовок";
        public override ObservableCollection<Parametr> Parametrs
        {
            get => base.Parametrs;
            set
            {
                base.Parametrs = value;
                OnPropertyChanged(nameof(Parametrs));
            }
        }
        public override Parametr ParametrSelected
        {
            get => base.ParametrSelected;
            set
            {
                base.ParametrSelected = value;
                Value = value.Text;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(ParametrSelected));
            }
        }
        public string Error { get; set; }
        public override string Value { get => base.Value; set => base.Value = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ComboBoxValidationViewModel(string name, string title, int id) 
        {
            FieldName = name;
            Title = title;

            Parametrs = new ObservableCollection<Parametr>();
            Parametrs.AddRange(GetParametrs());

            var parametr = Parametrs.FirstOrDefault(x => x.ID == id);

            if (parametr != null)
            {
                Value = parametr.Text;
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

        private ObservableCollection<Parametr> GetParametrs()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            BaseEntityAPI api = assembly.CreateInstance($"LogisticControlSystemDesktop.REST.API.{FieldName.Substring(0, FieldName.Length - 2)}API") as BaseEntityAPI;
            
            var items = api.GetAll() as IEnumerable<object>;

            var parametrs = new ObservableCollection<Parametr>();

            foreach (var item in items)
            {
                int i = 0;
                foreach (var property in item.GetType().GetProperties())
                {
                    if (i == 0)
                    {
                        parametrs.Add(new Parametr()
                        {
                            ID = (int)property.GetValue(item),
                            Text = property.GetValue(item).ToString()
                        });
                    }
                    i++;
                }
            }

            return parametrs;
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
