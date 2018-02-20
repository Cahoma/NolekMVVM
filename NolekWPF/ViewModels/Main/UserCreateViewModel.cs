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

namespace NolekWPF.ViewModels.Main
{
    public class UserCreateViewModel : ViewModelBase, IUserCreateViewModel
    {
        private IUserRepository _userRepository;
        private IErrorDataService _errorDataService;
        private IUserLookupDataService _userLookupDataService;
        private IEventAggregator _eventAggregator;

        private Login _newuser;

        public ObservableCollection<UserLookup> Users { get; }

        private bool _hasChanges;

        public UserCreateViewModel(IUserRepository userRepository, IErrorDataService errorDataService, IUserLookupDataService userLookupDataService,
            IEventAggregator eventAggregator
            )
        {
            CreateUserCommand = new DelegateCommand(OnCreateUserExecute, OnUserCreateCanExecute);

            

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _userLookupDataService = userLookupDataService;
            _eventAggregator = eventAggregator;

            Users = new ObservableCollection<UserLookup>();

            NewUser = CreateNewUser();

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
            }
            catch
            {
                MessageBox.Show("What happened???");
            }
        }

        private bool OnUserCreateCanExecute()
        {
            //validate fields to disable/enable button
            return true;
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
            catch
            {
                MessageBox.Show("You messed up");
            }
        }

        private Login CreateNewUser() //calls the add method in the repository to insert new equipment and return it
        {           
            var user = new Login();
            ((DelegateCommand)CreateUserCommand).RaiseCanExecuteChanged();

            //default values
            //user.LoginId = 45;
            user.Role = "";
            user.Username = "";
            user.Password = "";

            _userRepository.Add(user); //context is aware of the equipment to add
            
            return user;
        }

        public ICommand CreateUserCommand { get; }

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
    }
}
