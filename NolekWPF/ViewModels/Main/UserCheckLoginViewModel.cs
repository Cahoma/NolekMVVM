using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
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
    public class UserCheckLoginViewModel : ViewModelBase, IUserCheckLoginViewModel
    {
        public ICommand CheckLoginCommand { get; }

        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;

        public Login CurrentUser { get; set; }
        public bool Authenticated { get; set; }

        public IUserUpdateAdminViewModel UserUpdateAdminViewModel { get; }

        public UserCheckLoginViewModel(IUserRepository userRepository, IErrorDataService errorDataService, IUserLookupDataService userLookupDataService,
            IEventAggregator eventAggregator, IUserUpdateAdminViewModel userUpdateAdminViewModel
            )
        {
            CheckLoginCommand = new DelegateCommand(OnCheckUserExecute);

            UserUpdateAdminViewModel = userUpdateAdminViewModel;

            _errorDataService = errorDataService;
            _userRepository = userRepository;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
        }

        private void OnCheckUserExecute()
        {
            if(Username == CurrentUser.Username && Password == CurrentUser.Password)
            {
                _eventAggregator.GetEvent<UserCheck>().Publish();
                Authenticated = true;
            }
            else
            {
                MessageBox.Show("Wrong Username/Password");
            }
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
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
    }
}
