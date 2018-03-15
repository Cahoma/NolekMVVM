using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Equipment.ViewModels
{
    public interface IEquipmentListViewModel
    {
        IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        ObservableCollection<EquipmentLookup> Equipments { get; }
        EquipmentLookup SelectedEquipment { get; set; }
        Login CurrentUser { get; set; }

        Task LoadAsync();
        Task LoadEquipmentChoiceAsync();

    }
}