using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Main
{
    public class UserUpdateAdminViewModel : ViewModelBase, IUserUpdateAdminViewModel
    {
        public ICommand ChangeCommand { get; }

        private IEnumerable<LoginRoleDto> _loginRoles;

        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;

        public Login CurrentUser { get; set; }

        public UserUpdateAdminViewModel(IUserRepository userRepository, IErrorDataService errorDataService,
            IEventAggregator eventAggregator
            )
        {
            ChangeCommand = new DelegateCommand(OnChangeUserExecute);

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            _eventAggregator.GetEvent<OpenUserUpdateAdminViewEvent>()
                .Subscribe(OnOpenUserUpdateAdminView);
            //_eventAggregator.GetEvent<UserCheck>().Subscribe(OnUserCheck);
           
        }

        public async void OnUserCheck(int loginId)
        {
            await LoadAsync(loginId);
            await LoadRolesAsync();
        }

        private async void OnOpenUserUpdateAdminView(int loginId)
        {
            await LoadAsync(loginId);
        }

        public async void OnChangeUserExecute()
        {
            try
            {
                await _userRepository.SaveAsync();
                Login = UpdateUser();
                //_eventAggregator.GetEvent<OpenComponentListViewEvent>().Publish();
            }
            catch (Exception e)
            {
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

        public Login UpdateUser()
        {
            _eventAggregator.GetEvent<AfterUserUpdated>().Publish();
            return new Login();
        }

        public async Task LoadAsync(int loginId)
        {
            try
            {
                Login = await _userRepository.GetByIdAsync(loginId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                //create new error object from the exception and add to DB
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

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        private Login _login;
        public Login Login
        {
            get { return _login; }
            private set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
    }
}
