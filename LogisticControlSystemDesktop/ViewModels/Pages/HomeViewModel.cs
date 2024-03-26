using LogisticControlSystemDesktop.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class HomeViewModel : BindableBase
    {
        public ObservableCollection<ActiveRouteData> ActiveRouteDatas { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel()
        {
            ActiveRouteDatas = new ObservableCollection<ActiveRouteData>();

            var converter = new BrushConverter();
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "T-20-005-122", Character = "И", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Иванов Иван Иванович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            OnPropertyChanged(nameof(ActiveRouteDatas));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
