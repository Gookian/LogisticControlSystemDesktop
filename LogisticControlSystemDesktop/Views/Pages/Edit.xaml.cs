using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using System;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleEdit.xaml
    /// </summary>
    public partial class Edit : UserControl
    {
        public Edit(int id, string screenName, BaseEntityAPI baseEntityAPI, Type type)
        {
            InitializeComponent();

            DataContext = new VehicleEditViewModel(this, id, screenName, baseEntityAPI, type);
        }
    }
}
