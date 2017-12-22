﻿using NolekWPF.Data.DataServices;
using NolekWPF.Equipment.ViewModels;
using NolekWPF.Model;
using NolekWPF.ViewModels.Component;
using Prism.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.Helpers;
using Prism.Events;
using NolekWPF.Events;
using NolekWPF.ViewModels.Customers;
using NolekWPF.ViewModels.Equipment;

namespace NolekWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //login properties
        private bool _isAuthenticated;
        private string _visibility;
        private string _menuvisibility;
        private string _Cvisibility;
        private string _Framevisibility;
        private Login _currentuser;

        //viewmodels to inject into pages
        public IEquipmentListViewModel EquipmentListViewModel { get; }
        public IEquipmentCreateViewModel EquipmentCreateViewModel { get; }
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        public IComponentListViewModel ComponentListViewModel { get; }
        public IComponentDetailViewModel ComponentDetailViewModel { get; }
        public IComponentCreateViewModel ComponentCreateViewModel { get; }
        public IAddRemoveComponentViewModel AddRemoveComponentViewModel { get; }
        public ICustomerCreateViewModel CustomerCreateViewModel { get; }
        public ICustomerListViewModel CustomerListViewModel { get; }

        private IUserLookupDataService _userLookupDataService;
        private IUserDataService _userDataService;
        private IEventAggregator _eventAggregator;

        public MainViewModel(IEquipmentListViewModel equipmentListViewModel,
            IEquipmentCreateViewModel equipmentCreateViewModel,
            IEquipmentDetailViewModel equipmentDetailViewModel, IComponentDetailViewModel componentDetailViewModel,
            IComponentCreateViewModel componentCreateViewModel, IComponentListViewModel componentListViewModel,
            IUserLookupDataService userLookupDataService, IEventAggregator eventAggregator, IUserDataService userDataService,
            IAddRemoveComponentViewModel addRemoveComponentViewModel, ICustomerCreateViewModel customerCreateViewModel, ICustomerListViewModel customerListViewModel)
        {
            EquipmentListViewModel = equipmentListViewModel;
            EquipmentCreateViewModel = equipmentCreateViewModel;
            EquipmentDetailViewModel = equipmentDetailViewModel;
            ComponentListViewModel = componentListViewModel;
            ComponentDetailViewModel = componentDetailViewModel;
            ComponentCreateViewModel = componentCreateViewModel;
            AddRemoveComponentViewModel = addRemoveComponentViewModel;
            CustomerCreateViewModel = customerCreateViewModel;
            CustomerListViewModel = customerListViewModel;

            _eventAggregator = eventAggregator;

            _userLookupDataService = userLookupDataService;
            _userDataService = userDataService;

            MenuVisibility = "Collapsed";
            Username = "UserSecretary";
            // can't set password no more sadface

            LoginCommand = new DelegateCommand(Login);
            LogoutCommand = new DelegateCommand(Logout);
        }

        public async Task LoadAsync() //method must be async when loading in async data and return a task
        {
            //load list data
            await EquipmentListViewModel.LoadAsync();
            await ComponentListViewModel.LoadAsync();
            await CustomerListViewModel.LoadAsync();
            await CustomerCreateViewModel.LoadEquipment();

            await EquipmentCreateViewModel.LoadTypesAsync();
            await EquipmentCreateViewModel.LoadConfigurationsAsync();
            await EquipmentCreateViewModel.LoadCategoriesAsync();

            await EquipmentDetailViewModel.LoadTypesAsync();
            await EquipmentDetailViewModel.LoadConfigurationsAsync();
            await EquipmentDetailViewModel.LoadCategoriesAsync();
        }

        public bool isAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                if (value != _isAuthenticated)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged();
                }
            }
        }

        public Login CurrentUser
        {
            get { return _currentuser; }
            set
            {
                _currentuser = value;
                OnPropertyChanged();
            }
        }

        public string Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        public string MenuVisibility
        {
            get { return _menuvisibility; }
            set
            {
                _menuvisibility = value;
                OnPropertyChanged();
            }
        }
        public string ComponentVisibility
        {
            get { return _Cvisibility; }
            set
            {
                _Cvisibility = value;
                OnPropertyChanged();
            }
        }
        public string FrameVisibility
        {
            get { return _Framevisibility; }
            set
            {
                _Framevisibility = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public void Logout()
        {
            FrameVisibility = "Collapsed";
            MenuVisibility = "Collapsed";
            _currentuser = null;
        }

        public async void Login()
        {
            //TODO check username and password vs database here.
            //If using membershipprovider then just call Membership.ValidateUser(UserName, Password)
            var lookup = await _userLookupDataService.GetUserLookupAsync();

            //List<User> Users = _userDataService.GetUser();
            foreach (var user in lookup)
            {
                if (user.Username == Username && user.Password == Password)
                {
                    isAuthenticated = true;
                    Visibility = "Collapsed";
                    FrameVisibility = "Visible";
                    MenuVisibility = "Visible";
                    break;
                }
            }
            if (isAuthenticated == false)
            {
                MessageBox.Show("Wrong username/password");
                Username = string.Empty;
                Password = string.Empty;
            }
            else if (isAuthenticated == true)
            {
                _currentuser = await _userDataService.GetByNameAsync(Username);
                _eventAggregator.GetEvent<AfterUserLogin>()
                        .Publish(_currentuser);
                Username = string.Empty;
                Password = string.Empty;
            }
        }

    }
}
