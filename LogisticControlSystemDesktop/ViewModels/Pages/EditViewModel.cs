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

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class EditViewModel
    {
        public string ScreenName { get; set; }

        public ObservableCollection<UserControl> FormFields { get; set; }

        public DelegateCommand SaveClick { get; set; }

        public delegate void SavedHandler();
        public event SavedHandler OnSaved;

        private List<StructureItem> _structureItems;
        private BaseEntityAPI _baseEntityAPI1;
        private UserControl _view;
        private Type _type;
        private int _id;

        public EditViewModel(UserControl view, int id, string screanName, BaseEntityAPI baseEntityAPI, Type typeItem)
        {
            _view = view;
            _baseEntityAPI1 = baseEntityAPI;
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
                    FormFields.Add(new TextBoxValidation(item.Name, item.Title, ""));
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

            var result = _baseEntityAPI1.Edit(instance);

            if (result != null)
            {
                OnSaved?.Invoke();
                Navigator.Instance.Close(_view);
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
