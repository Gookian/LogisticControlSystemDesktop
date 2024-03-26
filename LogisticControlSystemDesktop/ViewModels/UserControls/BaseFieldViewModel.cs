using Prism.Mvvm;

namespace LogisticControlSystemDesktop.ViewModels.UserControls
{
    public abstract class BaseFieldViewModel : BindableBase
    {
        public virtual string FieldName { get; set; }
        public virtual string Value { get; set; }
    }
}
