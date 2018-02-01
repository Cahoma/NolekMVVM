using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.ViewModels.Customers
{
    public interface IAddRemoveEquipmentToFromCustomerViewModel
    {
        Login CurrentUser { get; set; }
        ObservableCollection<EquipmentLookup> CustomerEquipments { get; set; }
        ObservableCollection<CustomerDto> Customers { get; set; }
        ObservableCollection<CustomerDepartmentDto> Departments { get; set; }
        ObservableCollection<EquipmentLookup> Equipments { get; set; }
        ICollectionView EquipmentView { get; }
        string FilterString { get; set; }
        CustomerDto SelectedCustomer { get; set; }
        EquipmentLookup SelectedEquipment { get; set; }

        bool Filter(object obj);
        Task LoadAsync();
        Task LoadEquipmentAsync();
    }
}