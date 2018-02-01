using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Customers
{
    public class AddRemoveEquipmentToFromCustomerViewModel : ViewModelBase, IAddRemoveEquipmentToFromCustomerViewModel
    {
        private ICustomerRepository _customerRepository;
        private IEquipmentLookupDataService _equipmentLookupDataService;
        private IEventAggregator _eventAggregator;

        private IErrorDataService _errorDataService { get; }
        public ObservableCollection<CustomerDto> Customers { get; set; }
        public ObservableCollection<CustomerDepartmentDto> Departments { get; set; }
        public ObservableCollection<EquipmentLookup> Equipments { get; set; }
        public ObservableCollection<EquipmentLookup> CustomerEquipments { get; set; }

        public AddRemoveEquipmentToFromCustomerViewModel(ICustomerRepository customerRepository, IEquipmentLookupDataService equipmentLookupDataService,
            IErrorDataService errorDataService, IEventAggregator eventAggregator)
        {
            _customerRepository = customerRepository;
            _equipmentLookupDataService = equipmentLookupDataService;
            _errorDataService = errorDataService;
            _eventAggregator = eventAggregator;
            Customers = new ObservableCollection<CustomerDto>();
            Departments = new ObservableCollection<CustomerDepartmentDto>();
            Equipments = new ObservableCollection<EquipmentLookup>();
            CustomerEquipments = new ObservableCollection<EquipmentLookup>();
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin); //GET CURREMT USER

            AddEquipment = new DelegateCommand(OnAddEquipment);
        }
        //LOAD CUSTOMER LIST
        public async Task LoadAsync()
        {
            var collection = await _customerRepository.GetCustomers();
            Customers.Clear();
            foreach (var item in collection)
            {
                Customers.Add(item);
            }
        }
        //LOAD DEPARTMENTS LIST
        private void LoadDepartmentsForCustomer(int customerId)
        {
            CustomerDto customer = Customers.Where(c => c.CustomerID == customerId).SingleOrDefault();
            Departments.Clear();
            foreach (var item in customer.DepartmentsCollection)
            {
                Departments.Add(item);
            }
        }
        //LOAD EQUIPMENT LIST
        public async Task LoadEquipmentAsync()
        {
            try
            {
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
                Equipments.Clear();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                }
                EquipmentView = CollectionViewSource.GetDefaultView(Equipments);
                EquipmentView.Filter = new Predicate<object>(Filter);
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
        //LOAD EQUIPMENT FOR CUSTOMER
        private void LoadEquipmentForCustomer(int customerId)
        {
            CustomerDto customer = Customers.Where(c => c.CustomerID == customerId).SingleOrDefault();
            CustomerEquipments.Clear();
            foreach (var item in customer.EquipmentCollection)
            {
                CustomerEquipments.Add(item);
            }
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public ICommand AddEquipment { get; set; }

        public void OnAddEquipment()
        {
            Model.Equipment equipment = new Model.Equipment()
            {
                EquipmentId = SelectedEquipment.EquipmentId
            };
            CustomerEquipments.Add(SelectedEquipment);
            _customerRepository.AddCustomerEquipment(equipment, SelectedCustomer.CustomerID);
        }

        public Login CurrentUser { get; set; }

        //SELECTED EQUIPMENT TO ADD
        private EquipmentLookup _selectedEquipment;
        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged();
            }
        }
        //SELECTED CUSTOMER
        private CustomerDto _selectedCustomer { get; set; }
        public CustomerDto SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                LoadDepartmentsForCustomer(_selectedCustomer.CustomerID);
                LoadEquipmentForCustomer(_selectedCustomer.CustomerID);
            }
        }

        //FILTER EQUIPMENT LIST FROM SEARCH STRING
        public ICollectionView EquipmentView { get; private set; }
        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                FilterCollection();
            }
        }

        private void FilterCollection()
        {
            if (EquipmentView != null)
            {
                EquipmentView.Refresh();
            }
        }

        public bool Filter(object obj)
        {
            var data = obj as EquipmentLookup;

            if (EquipmentView != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    string allcaps = _filterString.ToUpper();
                    return data.TypeName.Contains(_filterString) || data.TypeName.Contains(allcaps);
                }
                return true;
            }
            return false;
        }
    }
}