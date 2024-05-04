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
using System.Collections.Generic;
using LogisticControlSystemDesktop.Models.Navigators;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class EditViewModel
    {
        public string ScreenName { get; set; }

        public ObservableCollection<UserControl> FormFields { get; set; }

        public DelegateCommand SaveClick { get; set; }

        private List<StructureItem> _structureItems;
        private BaseEntityAPI _baseEntityAPI;
        private UserControl _view;
        private Type _type;
        private int _id;

        public EditViewModel(UserControl view, int id, string screanName, BaseEntityAPI baseEntityAPI, Type typeItem)
        {
            _view = view;
            _baseEntityAPI = baseEntityAPI;
            _type = typeItem;
            _id = id;

            ScreenName = screanName;

            FormFields = new ObservableCollection<UserControl>();

            SaveClick = new DelegateCommand(Save_Click);

            LoadStructure(baseEntityAPI);
            LoadData(baseEntityAPI, id);
        }

        private void LoadStructure(BaseEntityAPI api)
        {
            _structureItems = api.GetStructure() as List<StructureItem>;

            foreach (var item in _structureItems)
            {
                if (!item.Name.EndsWith("Id"))
                {
                    FormFields.Add(new TextBoxValidation(item.Name, item.Title, item.Hint, "", item.Min, item.Max, item.Pattern));
                }
                else
                {
                    FormFields.Add(new ComboBoxValidation(item.Name, item.Title, 1));
                }
            }
        }

        private void LoadData(BaseEntityAPI api, int id)
        {
            var data = api.Get(id);
            var type = data.GetType();
            foreach (var item in _structureItems)
            {
                var property = _type.GetProperty(item.Name);
                var value = property.GetValue(data);

                var viewModel = FindFieldViewModelByName(item.Name);

                switch (viewModel.GetType().Name)
                {
                    case nameof(ComboBoxValidationViewModel):
                        viewModel.SetSelected((int)value);
                        break;
                    case nameof(TextBoxValidationViewModel):
                        viewModel.Value = value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private BaseFieldViewModel FindFieldViewModelByName(string name)
        {
            var userControl = FormFields.FirstOrDefault(x => (x.DataContext as BaseFieldViewModel).FieldName == name);
            var viewModel = (userControl.DataContext as BaseFieldViewModel);

            return viewModel;
        }

        private void Save_Click()
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
                Save();
            }
        }

        private void Save()
        {
            var instance = Activator.CreateInstance(_type);

            var namePropertyId = _type.Name + "Id";
            var propertyId = _type.GetProperty(namePropertyId);

            propertyId.SetValue(instance, _id);

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
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            var result = _baseEntityAPI.Edit(_id, instance);

            if (result != null)
            {
                MainNavigator.Instance.Close(_view);
            }
            else
            {
                MessageBox.Show("Не удалось изменить запись!");
            }
        }
    }
}
