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

        public virtual ObservableCollection<IdTargetValueItem> Parametrs { get; set; }
        public virtual IdTargetValueItem ParametrSelected { get; set; }

        public virtual void SetSelected(int id)
        {

        }
    }
}
