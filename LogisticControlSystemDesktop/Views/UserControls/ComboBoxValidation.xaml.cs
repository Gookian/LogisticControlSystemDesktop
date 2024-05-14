using LogisticControlSystemDesktop.ViewModels.UserControls;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TextBoxValidation.xaml
    /// </summary>
    public partial class ComboBoxValidation : UserControl
    {
        public ComboBoxValidation(string name, string title, string hint, int value)
        {
            InitializeComponent();

            BaseFieldViewModel viewModel = new ComboBoxValidationViewModel(name, title, hint, value);
            DataContext = viewModel;
        }
    }
}
