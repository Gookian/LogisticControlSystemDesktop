using LogisticControlSystemDesktop.Models;
using LogisticControlSystemDesktop.Views.Pages;
using LogisticControlSystemDesktop.Views.UserControls;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public string DisplayName { get; set; } = "Система контроля логистики";

        public DelegateCommand<string> Open { get; set; }

        private Guid _target;
        private StackPanel _navigatePanel;
        private Dictionary<Guid, ButtonNavigate> _buttonNavigates = new Dictionary<Guid, ButtonNavigate>();
        private Dictionary<string, UserControl> _registerScreen = new Dictionary<string, UserControl>();

        public ShellViewModel(Border border, StackPanel navigatePanel)
        {
            _navigatePanel = navigatePanel;

            Open = new DelegateCommand<string>(Open_Click);

            Navigator navigator = new Navigator(border, new Home());
            navigator.OnOpen += Navigator_OnOpen;
            navigator.OnNavigate += Navigator_OnNavigate;
            navigator.OnClose += Navigator_OnClose;

            navigator.Open(new Home(), "Главная");

            _registerScreen.Add("Пустая страница", new UnimplementedFunctionalityStub());
            _registerScreen.Add("Главная", new Home());
            _registerScreen.Add("Управление транспортными средствами", new VehicleManagement());
        }

        public void Open_Click(string title)
        {
            try
            {
                var screen = _registerScreen[title];

                Navigator.Instance.Open(screen, title);
            }
            catch (Exception) { }
        }

        private void Navigator_OnNavigate(Guid id)
        {
            _target = id;

            SetAccentColor();
        }

        private void Navigator_OnOpen(Guid id, string title)
        {
            _target = id;

            var newButton = new ButtonNavigate(id, title);

            _buttonNavigates.Add(id, newButton);
            _navigatePanel.Children.Add(newButton);

            SetAccentColor();
        }

        private void Navigator_OnClose(Guid id)
        {
            var button = _buttonNavigates[id];

            _navigatePanel.Children.Remove(button);
            _buttonNavigates.Remove(id);

            var item = _buttonNavigates.LastOrDefault();

            _target = item.Key;

            Navigator.Instance.Navigate(_target);

            SetAccentColor();
        }

        private void SetAccentColor()
        {
            foreach (var button in _buttonNavigates)
            {
                var viewModel = (button.Value.DataContext as ButtonNavigateViewModel);

                if (button.Key == _target)
                {
                    viewModel.Color = viewModel.AccentColor;
                }
                else
                {
                    viewModel.Color = viewModel.MainColor;
                }
            }
        }
    }
}