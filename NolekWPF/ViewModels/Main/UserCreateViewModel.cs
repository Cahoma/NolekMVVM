using System;
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

        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IUserLookupDataService _userLookupDataService;
        private IEventAggregator _eventAggregator;

        private IEnumerable<LoginRoleDto> _loginRoles;

        private bool _hasChanges;
        private Login _newuser;
        public Login CurrentUser { get; set; }

        public ObservableCollection<UserLookup> Users { get; }
        public ICollectionView UserView { get; private set; }

        public IUserUpdateAdminViewModel UserUpdateAdminViewModel { get; }

        public UserCreateViewModel(IUserRepository userRepository, IErrorDataService errorDataService, IUserLookupDataService userLookupDataService,
            IEventAggregator eventAggregator, IUserUpdateAdminViewModel userUpdateAdminViewModel
            )
        {
            CreateUserCommand = new DelegateCommand(OnCreateUserExecute, OnUserCreateCanExecute);
            //UpdateUserCommand = new DelegateCommand(OnUpdateUserExecute, OnUserUpdateCanExecute);

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _userLookupDataService = userLookupDataService;
            _eventAggregator = eventAggregator;
            UserUpdateAdminViewModel = userUpdateAdminViewModel;

            Users = new ObservableCollection<UserLookup>();
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            _eventAggregator.GetEvent<AfterUserUpdated>().Subscribe(RefreshList);

            NewUser = CreateNewUser();

            _eventAggregator.GetEvent<UserCheck>().Subscribe(OnUserCheck);
        }

        public async void OnUserCheck()
        {
            await LoadAsync();
            await LoadRolesAsync();
        }

        private Login CreateNewUser() //calls the add method in the repository to insert new equipment and return it
        {
            var user = new Login();
            ((DelegateCommand)CreateUserCommand).RaiseCanExecuteChanged();

            //default values
            user.Active = true;
            user.RoleId = 3;
            _userRepository.Add(user); //context is aware of the equipment to add

            return user;
        }

        private bool OnUserCreateCanExecute()
        {
            if (NewUser != null)
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
                RefreshList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool OnUserUpdateCanExecute()
        {
            //validate fields to disable/enable button
            if (NewUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private async void OnUpdateUserExecute()
        //{
        //    try
        //    {
        //        await _userRepository.SaveAsync();
        //        //NewUser = UpdateUser2();

        //        MessageBox.Show("User was successfully updated.");
        //        RefreshList();
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                MessageBox.Show(validationError.PropertyName + validationError.ErrorMessage + dbEx.Message);
        //            }
        //        }
        //    }
        //}

        //private Login UpdateUser2()
        //{
        //    var user = new Login();
        //    //user.Username = ChangeUser.Username;
        //    //user.Password = ChangeUser.Password;
        //    //user.RoleId = ChangeUser.RoleId;
        //    //user.Active = ChangeUser.Active;
        //    //user.LoginId = ChangeUser.LoginId;
        //    //((DelegateCommand)UpdateUserCommand).RaiseCanExecuteChanged();

        //    ////_equipmentRepository.Update(equipment);
        //    //return user;
        //    return user;
        //}


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

        public async Task LoadRolesAsync()
        {
            var roles = await _userRepository.GetLoginRoleAsync();
            LoginRoles = roles;
        }

        public async void RefreshList()
        {
            await LoadAsync();
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
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

        public IEnumerable<LoginRoleDto> LoginRoles
        {
            get { return _loginRoles; }
            private set
            {
                _loginRoles = value;
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
                //if(_selectedUser != null)
                //{
                //    LoadAsync(_selectedUser.LoginId);
                //    //NewUser = ToUpdateUser(_selectedUser);
                //}

                if (_selectedUser != null && CurrentUser.RoleId == 3)
                {
                    _eventAggregator.GetEvent<OpenUserUpdateAdminViewEvent>()
                        .Publish(_selectedUser.LoginId);
                }
            }
        }

        public async Task LoadAsync(int loginId)
        {
            try
            {
                NewUser = await _userRepository.GetByIdAsync(loginId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace,
                    LoginId = CurrentUser.LoginId
                };
                await _errorDataService.AddError(error);
            }
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
    }
}
