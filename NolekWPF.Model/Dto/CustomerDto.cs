using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Model.Dto
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public List<CustomerDepartment> DepartmentsList { get; set; }
        public List<Equipment> EquipmentsList { get; set; }

        private ObservableCollection<CustomerDepartmentDto> _departmentsCollection;

        public ObservableCollection<CustomerDepartmentDto> DepartmentsCollection
        {
            get
            {
                var modelCollection = new ObservableCollection<CustomerDepartment>(DepartmentsList);
                _departmentsCollection = new ObservableCollection<CustomerDepartmentDto>();
                foreach (var item in modelCollection)
                { //create dto objects from model objects
                    _departmentsCollection.Add(new CustomerDepartmentDto()
                    {
                        DepartmentId = item.CustomerDepartmentId,
                        DepartmentName = item.CustomerDepartmentName,
                        Country = item.Country.ContryName,
                        Address = item.Address,
                        City = item.City
                    });
                }
                return _departmentsCollection;
            }
            set
            {
                _departmentsCollection = value;
            }
        }

        private ObservableCollection<EquipmentLookup> _equipmentCollection;
        public ObservableCollection<EquipmentLookup> EquipmentCollection
        {
            get
            {
                var modelCollection = new ObservableCollection<Equipment>(EquipmentsList);
                _equipmentCollection = new ObservableCollection<EquipmentLookup>();
                foreach (var item in modelCollection)
                {//create dto objects from model objects
                    _equipmentCollection.Add(new EquipmentLookup()
                    {
                        EquipmentId = item.EquipmentId,
                        DateCreated = item.EquipmentDateCreated,
                        MainEquipmentNumber = item.EquipmentMainEquipmentNumber,
                        SerialNumber = item.EquipmentSerialnumber,
                        Status = item.EquipmentStatus,
                        TypeName = item.EquipmentType.EquipmentTypeName,
                        Category = item.EquipmentCategory.CategoryName,
                        Configuration = item.EquipmentConfiguration.EquipmentConfigurationDescription,
                        ContactPersonId = item.ContactPersonId,
                    });
                }
                return _equipmentCollection;
            }
            set
            {
                _equipmentCollection = value;
            }
        }

    }
}
