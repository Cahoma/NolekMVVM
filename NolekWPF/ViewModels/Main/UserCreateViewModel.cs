﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;
using NolekWPF.Wrappers;
using Prism.Events;
using NolekWPF.Events;
using NolekWPF.ViewModels;
using NolekWPF.Data.Repositories;
using NolekWPF.Data.DataServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NolekWPF.ViewModels.Main
{
    public class UserCreateViewModel : ViewModelBase, IUserCreateViewModel
    {
        public ICommand CreateUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand ChangeCommand { get; }

        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IUserLookupDataService _userLookupDataService;
        private IEventAggregator _eventAggregator;

        private IEnumerable<LoginRoleDto> _loginRoles;

        private Login _newuser;
        private Login _changeUser;

        public ObservableCollection<UserLookup> Users { get; }
        public ICollectionView UserView { get; private set; }
        public Login CurrentUser { get; set; }

        private bool _hasChanges;

        public UserCreateViewModel(IUserRepository userRepository, IErrorDataService errorDataService, IUserLookupDataService userLookupDataService,
            IEventAggregator eventAggregator
            )
        {
            CreateUserCommand = new DelegateCommand(OnCreateUserExecute, OnUserCreateCanExecute);
            UpdateUserCommand = new DelegateCommand(OnUpdateUserExecute, OnUserUpdateCanExecute);
            ChangeCommand = new DelegateCommand(OnChangeUserExecute, OnUserChangeCanExecute);

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _userLookupDataService = userLookupDataService;
            _eventAggregator = eventAggregator;

            Users = new ObservableCollection<UserLookup>();
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);

            NewUser = CreateNewUser();
        }

        private bool OnUserChangeCanExecute()
        {
            //validate fields to disable/enable button
            if (CurrentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void OnChangeUserExecute()
        {
            try
            {
                await _userRepository.SaveAsync();
                CurrentUser = UpdateUser2();

                MessageBox.Show("User was successfully updated.");
                await RefreshList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public async Task LoadRolesAsync()
        {
            var roles = await _userRepository.GetLoginRoleAsync();
            LoginRoles = roles;
        }


        public IEnumerable<LoginRoleDto> LoginRoles
        {
            get { return _loginRoles; }
            private set
            {
                _loginRoles = value;
                OnPropertyChanged();
            }
        }

        public async Task RefreshList()
        {
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _userLookupDataService.GetUserLookupAsync();
                int i = 0;
                Users.Clear();
                foreach (var item in lookup)
                {
                    Users.Add(item);
                    //var convert = cv.Convert(Equipments[i].ImagePath);
                    i++;
                }
                UserView = CollectionViewSource.GetDefaultView(Users);
            }
            catch
            {
                MessageBox.Show("What happened???");
            }
        }

        private bool OnUserUpdateCanExecute()
        {
            //validate fields to disable/enable button
            if(ChangeUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void OnUpdateUserExecute()
        {
            try
            {               
                await _userRepository.SaveAsync();
                ChangeUser = UpdateUser2();

                MessageBox.Show("User was successfully updated.");
                await RefreshList();
            }
            catch(DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show(validationError.PropertyName + validationError.ErrorMessage);
                    }
                }
            }
        }

        private bool OnUserCreateCanExecute()
        {
            if(NewUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void OnCreateUserExecute()
        {
            try
            {
                await _userRepository.SaveAsync();
                NewUser = CreateNewUser();

                MessageBox.Show("User was successfully created.");
                await RefreshList();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private Login CreateNewUser() //calls the add method in the repository to insert new equipment and return it
        {           
            var user = new Login();
            ((DelegateCommand)CreateUserCommand).RaiseCanExecuteChanged();

            //default values
            user.Active = true;

            _userRepository.Add(user); //context is aware of the equipment to add
            
            return user;
        }

        private Login UpdateUser2()
        {
            var user = new Login();
            user.Username = ChangeUser.Username;
            user.Password = ChangeUser.Password;
            user.RoleId = ChangeUser.RoleId;
            user.Active = ChangeUser.Active;
            user.LoginId = ChangeUser.LoginId;
            ((DelegateCommand)UpdateUserCommand).RaiseCanExecuteChanged();

            //_equipmentRepository.Update(equipment);
            return user;
        }

        private Login ToUpdateUser(UserLookup uuser) //calls the add method in the repository to insert new equipment and return it
        {
            var user = new Login();
            user.LoginId = uuser.LoginId;
            user.Username = uuser.Username;
            user.Password = uuser.Password;
            user.RoleId = uuser.RoleId;
            user.Active = uuser.Active;

            ((DelegateCommand)UpdateUserCommand).RaiseCanExecuteChanged();

            //_userRepository.SaveAsync(); //context is aware of the equipment to add
            return user;
        }

        public bool HasChanges //is true if changes has been made to equipment
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                }
            }
        }

        public Login NewUser
        {
            get { return _newuser; }
            set
            {
                _newuser = value;
                OnPropertyChanged();
            }
        }

        public Login ChangeUser
        {
            get { return _changeUser; }
            set
            {
                _changeUser = value;
                OnPropertyChanged();
            }
        }

        private UserLookup _selectedUser;

        public UserLookup SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if(_selectedUser != null)
                {
                    ChangeUser = ToUpdateUser(_selectedUser);
                    NewUser = ToUpdateUser(_selectedUser);
                }             
            }
        }
    }
}
