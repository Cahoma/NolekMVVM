using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using System.Collections.Generic;

namespace NolekWPF.Data.Repositories
{
    public interface IComponentRepository
    {
        void Add(Component component);
        Task<Component> GetByIdAsync(int compId);
        bool HasChanges();
        void Remove(Component model);
        Task<IEnumerable<ComponentLookupDto>> GetComponentChoiceAsync();

        Task SaveAsync();
    }
}