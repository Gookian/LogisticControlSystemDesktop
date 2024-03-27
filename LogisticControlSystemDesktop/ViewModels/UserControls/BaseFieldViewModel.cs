using LogisticControlSystemDesktop.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public abstract class BaseFieldViewModel : BindableBase
    {
        public virtual string FieldName { get; set; }
        public virtual string Text { get; set; }
        public virtual string Value { get; set; }

        public virtual ObservableCollection<Parametr> Parametrs { get; set; }
        public virtual Parametr ParametrSelected { get; set; }
    }
}
