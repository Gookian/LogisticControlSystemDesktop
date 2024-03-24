﻿using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using LogisticControlSystemDesktop.ViewModels;
using MahApps.Metro.IconPacks.Converter;
using MahApps.Metro.IconPacks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            DataContext = new HomeViewModel();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            // Настройки для компонента GMap
            mapControl.Bearing = 0;
            // Перетаскивание левой кнопки мыши
            mapControl.CanDragMap = true;
            // Перетаскивание карты левой кнопкой мыши
            mapControl.DragButton = System.Windows.Input.MouseButton.Left;

            //mapControl.GrayScaleMode = true;

            // Все маркеры будут показаны
            //mapControl.MarkersEnabled = true;
            // Максимальное приближение
            mapControl.MaxZoom = 18;
            // Минимальное приближение
            mapControl.MinZoom = 2;
            // Курсор мыши в центр карты
            mapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

            // Отключение нигативного режима
            //mapControl.NegativeMode = false;
            // Разрешение полигонов
            //mapControl.PolygonsEnabled = true;
            // Разрешение маршрутов
            //mapControl.RoutesEnabled = true;
            // Скрытие внешней сетки карты
            mapControl.ShowTileGridLines = false;
            // При загрузке 10-кратное увеличение
            mapControl.Zoom = 11;
            // Убрать красный крестик по центру
            mapControl.ShowCenter = false;

            // Чья карта используется
            mapControl.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            mapControl.Position = new PointLatLng(56.4846232334955, 84.9474585056305);

            //gmap.CacheLocation = @"C:\Users\PC\Desktop\Проекты\OSM\WindowsFormsApp1\WindowsFormsApp1\obj\Debug";
            //gmap.CacheLocation = @"C:\Users\PC\Desktop\Проекты\OSM\WindowsFormsApp1\WindowsFormsApp1\obj\Debug\TileDBv5\en\Data.gmdb";


            // Для запросов
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
            /*try
            {
                System.Net.IPHostEntry ipHost = System.Net.Dns.GetHostEntry("ditu.google.cn");
            }
            catch
            {
                mapControl.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            mapControl.MapProvider = GMapProviders.GoogleChinaMap; //гугл карта китай
            mapControl.MinZoom = 2;  //Минимальный зум
            mapControl.MaxZoom = 17; //Максимальный зум
            mapControl.Zoom = 5;     //Текущий зум
            mapControl.ShowCenter = false; //Не показывать центральный крест
            mapControl.DragButton = MouseButton.Left; //Щелкните левой кнопкой мыши, чтобы перетащить карту
            mapControl.Position = new PointLatLng(32.064, 118.704); //Центральное расположение карты: Нанкин.
            */
            mapControl.MouseRightButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(mapControl);
            PointLatLng point = mapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
            //MessageBox.Show(point.Lat + " " + point.Lng);
            GMapMarker marker = new GMapMarker(point);

            var icon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.Truck
            };
            icon.Width = 30;
            icon.Height = 30;

            marker.Shape = icon;
            marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);

            mapControl.Markers.Add(marker);
        }
    }

    public class Converter : PackIconMaterialKindToImageConverter
    {
        public ImageSource Convert(string iconCind, Brush foregroundBrush)
        {
            return base.CreateImageSource(iconCind, foregroundBrush);
        }
    }
}