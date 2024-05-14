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
using System.Text.RegularExpressions;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class CreateViewModel : BindableBase, INotifyPropertyChanged
    {
        public string ScreenName { get; set; }

        public ObservableCollection<UserControl> FormFields { get; set; }

        public DelegateCommand CreateClick { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<StructureItem> _structureItems;
        private BaseEntityAPI _baseEntityAPI;
        private UserControl _view;
        private Type _type;

        public CreateViewModel(UserControl view, string screanName, BaseEntityAPI baseEntityAPI, Type typeItem)
        {
            _view = view;
            _baseEntityAPI = baseEntityAPI;
            _type = typeItem;

            ScreenName = screanName;

            FormFields = new ObservableCollection<UserControl>();

            CreateClick = new DelegateCommand(Create_Click);

            LoadStructure(baseEntityAPI);
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
                MainNavigator.Instance.Close(_view);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись!");
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
