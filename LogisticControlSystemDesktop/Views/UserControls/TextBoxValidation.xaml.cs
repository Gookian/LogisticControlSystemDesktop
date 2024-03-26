using LogisticControlSystemDesktop.ViewModels.UserControls;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TextBoxValidation.xaml
    /// </summary>
    public partial class TextBoxValidation : UserControl
    {
        public TextBoxValidation(string name, string title, string value)
        {
            InitializeComponent();

            BaseFieldViewModel viewModel = new TextBoxValidationViewModel(name, title, value);
            DataContext = viewModel;
        }
    }
}
