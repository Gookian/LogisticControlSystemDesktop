using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using System;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleEdit.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        public Create(string screenName, BaseEntityAPI baseEntityAPI, Type type)
        {
            InitializeComponent();

            DataContext = new VehicleCreateViewModel(this, screenName, baseEntityAPI, type);
        }
    }
}
