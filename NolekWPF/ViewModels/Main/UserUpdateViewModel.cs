﻿using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.ViewModels.Main;
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
    public class UserUpdateViewModel : ViewModelBase, IUserUpdateViewModel
    {
        public ICommand ChangeCommand { get; }

        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;

        public Login CurrentUser { get; set; }

        public UserUpdateViewModel(IUserRepository userRepository, IErrorDataService errorDataService,
            IEventAggregator eventAggregator
            )
        {
            ChangeCommand = new DelegateCommand(OnChangeUserExecute);

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
        }


        public void OnChangeUserExecute()
        {

        }

        private async void OnLogin(Login user)
        {
            CurrentUser = user;
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                User = CurrentUser;
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

        private Login _user;
        public Login User
        {
            get { return _user; }
            private set
            {
                _user = value;
                OnPropertyChanged();
            }
        }


    }
}