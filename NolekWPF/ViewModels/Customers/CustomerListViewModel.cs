using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels.Customers
{
    public class CustomerListViewModel : ViewModelBase, ICustomerListViewModel
    {
        public ICustomerDataService _customerDataService { get; }
        public IErrorDataService _errorDataService { get; }
        public ICustomerRepository _customerRepository { get; }
        public ObservableCollection<CustomerDto> Customers { get; set; }
        public ObservableCollection<EquipmentLookup> Equipments { get; set; }
        public ObservableCollection<CustomerDepartmentDto> Departments { get; set; }

        public CustomerListViewModel(ICustomerDataService customerDataService, IErrorDataService errorDataService, ICustomerRepository customerRepository)
        {
            _customerDataService = customerDataService;
            _errorDataService = errorDataService;
            _customerRepository = customerRepository;
            Customers = new ObservableCollection<CustomerDto>();
            Equipments = new ObservableCollection<EquipmentLookup>();
            Departments = new ObservableCollection<CustomerDepartmentDto>();
        }

        private CustomerDto _selectedCustomer;
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

        private void LoadDepartmentsForCustomer(int customerId)
        {
            CustomerDto customer = Customers.Where(c => c.CustomerID == customerId).SingleOrDefault();
            Departments.Clear();
            foreach (var item in customer.DepartmentsCollection)
            {
                Departments.Add(item);
            }
        }
        private void LoadEquipmentForCustomer(int customerId)
        {
            CustomerDto customer = Customers.Where(c => c.CustomerID == customerId).SingleOrDefault();
            Equipments.Clear();
            foreach (var item in customer.EquipmentCollection)
            {
                Equipments.Add(item);
            }
        }

        public async Task LoadAsync()
        {
            var collection = await _customerRepository.GetCustomers();
            Customers.Clear();
            foreach (var item in collection)
            {
                Customers.Add(item);
            }
        }
    }
}
