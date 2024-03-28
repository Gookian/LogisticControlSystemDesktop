﻿using LogisticControlSystemDesktop.Models.REST.API;
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
using System.Reflection;
using System.Windows.Media;
using System.Net.Http;

namespace LogisticControlSystemDesktop.ViewModels.Pages
{
    public class CreateViewModel : BindableBase, INotifyPropertyChanged
    {
        public string ScreenName { get; set; }

        public ObservableCollection<UserControl> FormFields { get; set; }

        public DelegateCommand CreateClick { get; set; }

        public delegate void CreateHandler(object item);
        public event CreateHandler OnCreated;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<StructureItem> _structureItems;
        private BaseEntityAPI _baseEntityAPI1;
        private UserControl _view;
        private Type _type;

        public CreateViewModel(UserControl view, string screanName, BaseEntityAPI baseEntityAPI, Type typeItem)
        {
            _view = view;
            _baseEntityAPI1 = baseEntityAPI;
            _type = typeItem;

            ScreenName = screanName;

            FormFields = new ObservableCollection<UserControl>();

            CreateClick = new DelegateCommand(Create_Click);

            SubscribeOnCreated();
            LoadStructure(baseEntityAPI);
        }

        private void SubscribeOnCreated()
        {
            var navigateItems = Navigator.Instance.GetAllNavigateItem();

            foreach (var navigateItem in navigateItems)
            {
                if (navigateItem.Screen.DataContext.GetType().Name == $"{_type.Name}ManagementViewModel")
                {
                    var type = navigateItem.Screen.DataContext.GetType();
                    MethodInfo method = type.GetMethod("SignOnCreated", new Type[] { typeof(CreateViewModel) });
                    method.Invoke(navigateItem.Screen.DataContext, new object[] { this });
                }
            }
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

        private BaseFieldViewModel FindFieldViewModelByName(string name)
        {
            var userControl = FormFields.FirstOrDefault(x => (x.DataContext as BaseFieldViewModel).FieldName == name);
            var viewModel = (userControl.DataContext as BaseFieldViewModel);

            return viewModel;
        }

        private void Create_Click()
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
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            var result = _baseEntityAPI1.Create(instance);

            if (result != null)
            {
                OnCreated?.Invoke(result);
                Navigator.Instance.Close(_view);
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
