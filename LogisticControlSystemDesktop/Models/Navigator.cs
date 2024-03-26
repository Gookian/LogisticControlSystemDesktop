using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LogisticControlSystemDesktop.Models
{
    public class Navigator
    {
        public static Navigator Instance { get; set; }

        public delegate void OpenHandler(Guid id, string title);
        public event OpenHandler OnOpen;

        public delegate void NavigateHandler(Guid id);
        public event NavigateHandler OnNavigate;

        public delegate void CloseHandler(Guid id);
        public event CloseHandler OnClose;

        private List<NavigateItem> _screens = new List<NavigateItem>();
        private Decorator _target;
        private UserControl _default;

        public Navigator(Decorator target, UserControl defaultScreen)
        {
            _target = target;
            _default = defaultScreen;

            if (Instance != this)
            {
                Instance = this;
            }
        }

        public Guid Open(UserControl screan, string title)
        {
            var item = new NavigateItem() {
                Id = Guid.NewGuid(),
                Title = title,
                Screen = screan
            };
            _screens.Add(item);
            _target.Child = item.Screen;

            OnOpen?.Invoke(item.Id, item.Title);

            return item.Id;
        }

        public void Navigate(Guid id)
        {
            var item = _screens.FirstOrDefault(s => s.Id == id);

            if (item != null)
            {
                _target.Child = item.Screen;

                OnNavigate?.Invoke(item.Id);
            }
        }

        public void Close(Guid id)
        {
            var item = _screens.FirstOrDefault(s => s.Id == id);

            if (item != null && item != _screens.First())
            {
                _screens.Remove(item);

                OnClose?.Invoke(item.Id);
            }
        }

        public void Close(UserControl screan)
        {
            var item = _screens.FirstOrDefault(s => object.ReferenceEquals(s.Screen, screan));

            if (item != null && item != _screens.First())
            {
                _screens.Remove(item);

                OnClose?.Invoke(item.Id);
            }
        }
    }

    public class NavigateItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public UserControl Screen { get; set; }
    }
}
