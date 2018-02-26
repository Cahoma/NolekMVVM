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
        private IUserDataService _userDataService;

        private Login _newuser;

        public ObservableCollection<UserLookup> Users { get; }
        public ObservableCollection<Login> Users2 { get; }

        private bool _hasChanges;

        public UserCreateViewModel(IUserRepository userRepository, IErrorDataService errorDataService, IUserLookupDataService userLookupDataService,
            IEventAggregator eventAggregator, IUserDataService userDataService
            )
        {
            CreateUserCommand = new DelegateCommand(OnCreateUserExecute, OnUserCreateCanExecute);
            DeleteUserCommand = new DelegateCommand(OnDeleteUserExecute, OnUserDeleteCanExecute);

            _errorDataService = errorDataService;
            _userRepository = userRepository;
            _userLookupDataService = userLookupDataService;
            _eventAggregator = eventAggregator;
            _userDataService = userDataService;

            NewUser = CreateNewUser();

            //Users = new ObservableCollection<UserLookup>();
            Users2 = new ObservableCollection<Login>();
        }

        private Login _selectedUser;

        public Login SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                //if (_selectedUser != null && CurrentUser.Role == "Secretary")
                //{
                //    _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                //        .Publish(_selectedEquipment.EquipmentId);
                //}
            }
        }

        public async Task RefreshList()
        {
            await LoadAsync2();
        }

        public async Task LoadAsync2()
        {
            try
            {
                var lookup = await _userLookupDataService.GetUserLookupAsync();
                Login user = new Login();
                Users2.Clear();
                foreach (var item in lookup)
                {
                    user = await _userDataService.GetByIdAsync(item.LoginId);
                    Users2.Add(user);

                    //var convert = cv.Convert(Equipments[i].ImagePath);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //public async Task LoadAsync()
        //{
        //    try
        //    {
        //        var lookup = await _userLookupDataService.GetUserLookupAsync();
        //        int i = 0;
        //        Users.Clear();
        //        foreach (var item in lookup)
        //        {
        //            Users.Add(item);
        //            //var convert = cv.Convert(Equipments[i].ImagePath);
        //            i++;
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("What happened???");
        //    }
        //}

        private bool OnUserCreateCanExecute()
        {
            //validate fields to disable/enable button
            
            return true;
        }

        private async void OnCreateUserExecute()
        {
            try
            {            
                NewUser = CreateNewUser();
                await _userRepository.SaveAsync();
                //MessageBox.Show("User was successfully created.");
                await RefreshList();
            }
            catch
            {
                MessageBox.Show("You messed up");
            }
        }

        private bool OnUserDeleteCanExecute()
        {
            //validate fields to disable/enable button
            return true;
        }

        private async void OnDeleteUserExecute()
        {
            try
            {
                //_userRepository.Remove(Users2[(int)SelectedListUserIndex]); //remove from context

                await DeleteUser();
                await _userRepository.SaveAsync();

                    //SelectedListUserIndex = null;
                await RefreshList();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public async Task DeleteUser() 
        {
            if (_selectedUser == null)
            {
                return;        
            }
            var user = new Login();

            user = await _userDataService.GetByIdAsync(_selectedUser.LoginId);
            ((DelegateCommand)DeleteUserCommand).RaiseCanExecuteChanged();

            _userRepository.Remove(user); //context is aware of the equipment to add

        }

        private Login CreateNewUser() //calls the add method in the repository to insert new equipment and return it
        {           
            Login user = new Login();
            ((DelegateCommand)CreateUserCommand).RaiseCanExecuteChanged();

            //default values
            //user.LoginId = 45;
            user.Role = "";
            user.Password = "";
            user.Username = "";


            _userRepository.Add(user); //context is aware of the equipment to add

            return user;
        }

        public ICommand CreateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

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
