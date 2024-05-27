using LogisticControlSystemDesktop.Models.REST.API;
using System;
using LogisticControlSystemDesktop.Views.UserControls;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Prism.Commands;
using LogisticControlSystemDesktop.Models;
using System.Linq;
using LogisticControlSystemDesktop.ViewModels.UserControls;
using System.Windows;
using Prism.Mvvm;
using System.ComponentModel;
using System.Collections.Generic;
using LogisticControlSystemDesktop.Models.Navigators;
using System.Globalization;
using LogisticControlSystemDesktop.Views.Pages;
using System.Windows.Media;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class CreateOrderViewModel : BindableBase, INotifyPropertyChanged
    {
        public ObservableCollection<Grid> OrderDetails { get; set; }

        public string ScreenName { get; set; }

        public ObservableCollection<UserControl> FormFields { get; set; }

        public DelegateCommand CreateClick { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<StructureItem> _structureItems;
        private BaseEntityAPI _baseEntityAPI;
        private UserControl _view;
        private Type _type;

        public CreateOrderViewModel(UserControl view, string screanName, BaseEntityAPI baseEntityAPI, Type typeItem)
        {
            _view = view;
            _baseEntityAPI = baseEntityAPI;
            _type = typeItem;

            ScreenName = screanName;

            FormFields = new ObservableCollection<UserControl>();
            OrderDetails = new ObservableCollection<Grid>();

            CreateClick = new DelegateCommand(Create_Click);

            LoadStructure(baseEntityAPI);
            LoadPositionProductStart();
        }

        private void AddButton()
        {
            var button = new Button();
            button.Content = "Добавить позицию";
            button.Style = (Style)Application.Current.FindResource("CustomButtonStyle");
            button.Click += ButtonAddPositionProduct_Click;

            var stackPanelButton = new Grid();
            stackPanelButton.Children.Add(button);

            OrderDetails.Add(stackPanelButton);
        }

        private void LoadPositionProductStart()
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var stackPanelField = new StackPanel();
            stackPanelField.Children.Add(new ComboBoxValidation("ProductDataId", "Тип товара", "Выберите тип товара", 1));
            stackPanelField.Children.Add(new TextBoxValidation("sum", "Количество товара", "Введите количество товра", "1", 4, 1, ""));
            Grid.SetColumn(stackPanelField, 0);

            grid.Children.Add(stackPanelField);

            OrderDetails.Add(grid);
            AddButton();
        }

        private void LoadPositionProduct()
        {
            var leftColumn = new ColumnDefinition();
            var rightColumn = new ColumnDefinition();
            rightColumn.Width = new GridLength(100);

            var grid = new Grid();
            grid.Uid = "B" + (OrderDetails.Count).ToString();
            grid.ColumnDefinitions.Add(leftColumn);
            grid.ColumnDefinitions.Add(rightColumn);

            var stackPanelField = new StackPanel();
            stackPanelField.Children.Add(new ComboBoxValidation("ProductDataId", "Тип товара", "Выберите тип товара", 1));
            stackPanelField.Children.Add(new TextBoxValidation("sum", "Количество товара", "Введите количество товра", "1", 4, 1, ""));
            Grid.SetColumn(stackPanelField, 0);
            
            var button = new Button();
            button.Uid = "B" + (OrderDetails.Count).ToString();
            button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            button.Margin = new Thickness(10, 0, 0, 0);
            button.Height = 55;
            button.Content = "Удалить";
            button.Style = (Style)Application.Current.FindResource("CustomButtonStyle");
            button.Click += ButtonDeletePositionProduct_Click;
            Grid.SetColumn(button, 1);

            grid.Children.Add(stackPanelField);
            grid.Children.Add(button);

            OrderDetails.Add(grid);
            AddButton();
        }

        private void ButtonAddPositionProduct_Click(object sender, RoutedEventArgs e)
        {
            OrderDetails.RemoveAt(OrderDetails.Count - 1);

            LoadPositionProduct();
        }

        private void ButtonDeletePositionProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            //var id = Convert.ToInt32(button.Uid.Remove(0, 1));

            var element = OrderDetails.FirstOrDefault(x => x.Uid == button.Uid);

            OrderDetails.Remove(element);

            //OrderDetails.RemoveAt(id);
        }

        private void LoadStructure(BaseEntityAPI api)
        {
            _structureItems = api.GetStructure() as List<StructureItem>;

            foreach (var item in _structureItems)
            {
                if (!item.Name.EndsWith("Id"))
                {
                    FormFields.Add(new TextBoxValidation(item.Name, item.Title, item.Hint, "", item.Max, item.Min, item.Pattern));
                }
                else
                {
                    FormFields.Add(new ComboBoxValidation(item.Name, item.Title, item.Hint, 1));
                }
            }
        }

        private BaseFieldViewModel FindFieldViewModelByName(string name)
        {
            var userControl = FormFields.FirstOrDefault(x => (x.DataContext as BaseFieldViewModel).FieldName == name);
            var viewModel = (userControl.DataContext as BaseFieldViewModel);

            return viewModel;
        }

        private void Create_Click()
        {
            ValidateFields();
        }

        private void ValidateFields()
        {
            bool valid = true;
            foreach (var item in FormFields)
            {
                if (item is TextBoxValidation)
                {
                    var viewModel = item.DataContext as TextBoxValidationViewModel;
                    if (!viewModel.Validate())
                    {
                        valid = false;
                    }
                }
                else if (item is ComboBoxValidation)
                {
                    var viewModel = item.DataContext as ComboBoxValidationViewModel;
                }
            }

            if (valid)
            {
                Create();
            }
        }

        private void Create()
        {
            var instance = Activator.CreateInstance(_type);

            foreach (var item in _structureItems)
            {
                var viewModel = FindFieldViewModelByName(item.Name);

                var property = _type.GetProperty(item.Name);

                switch (viewModel.GetType().Name)
                {
                    case nameof(ComboBoxValidationViewModel):
                        property.SetValue(instance, Convert.ToInt32(viewModel.ParametrSelected.Id));
                        break;
                    case nameof(TextBoxValidationViewModel):
                        var propertyType = property.PropertyType;
                        var typeCode = Type.GetTypeCode(propertyType);

                        switch (typeCode)
                        {
                            case TypeCode.Int32:
                                property.SetValue(instance, Convert.ToInt32(viewModel.Value));
                                break;
                            case TypeCode.Double:
                                property.SetValue(instance, Convert.ToDouble(viewModel.Value));
                                break;
                            case TypeCode.String:
                                property.SetValue(instance, viewModel.Value);
                                break;
                            case TypeCode.DateTime:
                                DateTime date = DateTime.ParseExact(viewModel.Value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                property.SetValue(instance, date);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            var result = _baseEntityAPI.Create(instance);

            if (result != null)
            {
                CerateOrderDetails((result as Order).OrderId);
                MainNavigator.Instance.Close(_view);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись!");
            }
        }

        private bool CerateOrderDetails(int orderId)
        {
            foreach (var item in OrderDetails)
            {
                foreach (var child in item.Children)
                {
                    if (child is StackPanel)
                    {
                        var stackPanel = (StackPanel)child;
                        if (stackPanel != null)
                        {
                            var combobox = stackPanel.Children[0] as ComboBoxValidation;
                            var textbox = stackPanel.Children[1] as TextBoxValidation;

                            var comboboxViewModel = (combobox.DataContext as ComboBoxValidationViewModel);
                            var textboxViewModel = (textbox.DataContext as TextBoxValidationViewModel);

                            var productDataId = comboboxViewModel.ParametrSelected.Id;
                            var count = Convert.ToInt32(textboxViewModel.Value);

                            var orderDetail = new OrderDetail()
                            {
                                OrderId = orderId,
                                ProductDataId = productDataId,
                                Count = count,
                            };
                            OrderDetailAPI.Instance.Create(orderDetail);
                        }
                    }
                }
            }
            return true;
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
