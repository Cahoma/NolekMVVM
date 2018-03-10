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

namespace NolekWPF.Equipment.ViewModels
{
    public class EquipmentCreateViewModel : ViewModelBase, IEquipmentCreateViewModel
    {
        private EquipmentWrapper _equipment;
        private IEnumerable<EquipmentCategoryDto> _equipmentCategory;
        private IEnumerable<EquipmentConfigurationDto> _equipmentConfiguration;
        private IEnumerable<EquipmentTypeDto> _equipmentType;
        private IEquipmentRepository _equipmentRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;
        public Login CurrentUser { get; set; }

        public EquipmentCreateViewModel(IEquipmentRepository equipmentRepository, IErrorDataService errorDataService, IEventAggregator eventAggregator)
        {
            CreateEquipmentCommand = new DelegateCommand(OnCreateEquipmentExecute, OnEquipmentCreateCanExecute);
            _equipmentRepository = equipmentRepository;
            _errorDataService = errorDataService;
            Equipment = CreateNewEquipment(); //assign the equipment to add to the Equipment property   
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        //load up data for the combo boxes
        public async Task LoadTypesAsync()
        {
            var types = await _equipmentRepository.GetEquipmentTypesAsync();
            EquipmentTypes = types;
        }
        public async Task LoadConfigurationsAsync()
        {
            var configurations = await _equipmentRepository.GetEquipmentConfigurationsAsync();
            EquipmentConfigurations = configurations;
        }
        public async Task LoadCategoriesAsync()
        {
            var categories = await _equipmentRepository.GetEquipmentCategoriesAsync();
            EquipmentCategories = categories;
        }

        public EquipmentWrapper Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<EquipmentTypeDto> EquipmentTypes
        {
            get { return _equipmentType; }
            private set
            {
                _equipmentType = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<EquipmentCategoryDto> EquipmentCategories
        {
            get { return _equipmentCategory; }
            private set
            {
                _equipmentCategory = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<EquipmentConfigurationDto> EquipmentConfigurations
        {
            get { return _equipmentConfiguration; }
            private set
            {
                _equipmentConfiguration = value;
                OnPropertyChanged();
            }
        }

        private bool OnEquipmentCreateCanExecute()
        {
            //validate fields to disable/enable buton
            if (Equipment != null && !Equipment.HasErrors && CurrentUser.RoleId == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void OnCreateEquipmentExecute()
        {
            try
            {
                await _equipmentRepository.SaveAsync();
                Equipment = CreateNewEquipment();
                MessageBox.Show("Equipment was successfully created.");
                _eventAggregator.GetEvent<AfterEquipmentCreated>().Publish();
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

        public ICommand CreateEquipmentCommand { get; }

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

        private EquipmentWrapper CreateNewEquipment() //calls the add method in the repository to insert new equipment and return it
        {
            var equipment = new EquipmentWrapper(new Model.Equipment());

            //when property in equipment changes, and it has errors, disable the create button
            equipment.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Equipment.HasErrors))
                {
                    ((DelegateCommand)CreateEquipmentCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)CreateEquipmentCommand).RaiseCanExecuteChanged();

            //default values
            equipment.EquipmentSerialnumber = "";
            equipment.EquipmentStatus = false;
            equipment.EquipmentDateCreated = DateTime.Now;
            equipment.EquipmentCategoryId = 1;
            equipment.EquipmentConfigurationID = 1;
            equipment.EquipmentTypeID = 1;

            _equipmentRepository.Add(equipment.Model); //context is aware of the equipment to add
            return equipment;
        }
    }
}
