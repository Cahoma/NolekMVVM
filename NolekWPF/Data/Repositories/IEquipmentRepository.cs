using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public interface IEquipmentRepository
    {
        void Add(Model.Equipment equipment);
        List<EquipmentComponent> GetEquipmentComponents(int equipmentId);
        Task<Model.Equipment> GetByIdAsync(int equipId);
        Task<IEnumerable<EquipmentCategoryDto>> GetEquipmentCategoriesAsync();
        Task<IEnumerable<EquipmentConfigurationDto>> GetEquipmentConfigurationsAsync();
        Task<IEnumerable<EquipmentTypeDto>> GetEquipmentTypesAsync();
        Task<IEnumerable<ContactPersonDto>> GetEquipmentContactPersonsAsync();
        void RemoveEquipmentComponent(EquipmentComponent model);
        bool HasChanges();
        void Remove(Model.Equipment model);
        Task SaveAsync();
        void Update(Model.Equipment model);
        void UpdateComponents(ComponentDto model, int equipmentId);
        Task<IEnumerable<EquipmentSearchDto>> GetEquipmentChoiceAsync();
    }
}