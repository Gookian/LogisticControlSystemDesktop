using LogisticControlSystemDesktop.Models.REST.API;
using LogisticControlSystemDesktop.ViewModels.Pages;
using System;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehicleEdit.xaml
    /// </summary>
    public partial class CreateOrder : UserControl
    {
        public CreateOrder(string screenName, BaseEntityAPI baseEntityAPI, Type type)
        {
            InitializeComponent();

            DataContext = new CreateOrderViewModel(this, screenName, baseEntityAPI, type);
        }
    }
}
