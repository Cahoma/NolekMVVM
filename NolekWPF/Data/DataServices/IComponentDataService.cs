using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.DataServices
{
    public interface IComponentDataService
    {
        Task<Component> GetByIdAsync(int componentId);
        Task<IEnumerable<ComponentDto>> GetComponentLookupAsync();
        Task<IEnumerable<ComponentDto>> GetComponentsByEquipmentIdAsync(int equipmentId);

        //List<ComponentLookupDto> GetComponent();
    }
}