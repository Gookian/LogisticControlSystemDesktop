using LogisticControlSystemDesktop.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "X-20-432-456", Character = "М", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Майоров Александр Богданович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "C-23-546-546", Character = "Г", BgColor = (Brush)converter.ConvertFromString("#8b82e7"), Name = "Гордеева Александра Егоровна", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "B-33-456-456", Character = "Е", BgColor = (Brush)converter.ConvertFromString("#69f25c"), Name = "Еремин Мирон Евгеньевич", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "F-23-234-234", Character = "Ю", BgColor = (Brush)converter.ConvertFromString("#ae1d50"), Name = "Юдин Степан Захарович", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "G-33-456-456", Character = "О", BgColor = (Brush)converter.ConvertFromString("#0d52c3"), Name = "Орлова Валерия Ярославовна", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            ActiveRouteDatas.Add(new ActiveRouteData { NumberOrder = "A-43-234-244", Character = "С", BgColor = (Brush)converter.ConvertFromString("#ec3361"), Name = "Семенова Аиша Артёмовна", Address = "г. Томск ул. Пушкина 22", TemporaryСorridor = "10:00-19:00 24.03.2024", StateRoute = "В пути" });
            OnPropertyChanged(nameof(ActiveRouteDatas));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
