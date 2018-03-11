using NolekWPF.DataAccess;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class ComponentDataService : IComponentDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public ComponentDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Component> GetByIdAsync(int componentId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Components.AsNoTracking().SingleAsync(f => f.ComponentId == componentId);
            }
        }

        public async Task<IEnumerable<ComponentDto>> GetComponentLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Components.AsNoTracking().Select(f => new ComponentDto
                {
                    ComponentId = f.ComponentId,
                    ComponentDescription = f.ComponentDescription,
                    ComponentType = f.ComponentType,
                    ComponentOrderNumber = f.ComponentOrderNumber,
                    ComponentSerialNumber = f.ComponentSerialNumber,
                    ComponentSupplyNumber = f.ComponentSupplyNumber
                }).ToListAsync();
            }
        }
        

        //Get all components for a specific equipment
        public async Task<IEnumerable<ComponentDto>> GetComponentsByEquipmentIdAsync(int equipmentId)
        {
            using (var ctx = _contextCreator())
            {
                var result = from equipment in ctx.Equipments
                        .Where(equipment => equipment.EquipmentId == equipmentId)
                             join equipmentmodel in ctx.EquipmentComponents
                                 on equipment.EquipmentId equals equipmentmodel.EquipmentID
                             join model in ctx.Components
                                 on equipmentmodel.ComponentID equals model.ComponentId
                             select new ComponentDto()
                             {
                                 ComponentId = model.ComponentId,
                                 ComponentType = model.ComponentType,
                                 ComponentDescription = model.ComponentDescription,
                                 ComponentOrderNumber = model.ComponentOrderNumber,
                                 ComponentSerialNumber = model.ComponentSerialNumber,
                                 ComponentSupplyNumber = model.ComponentSupplyNumber
                             };
                return await result.ToListAsync(); // or whatever non-deferred you want
            }
        }



        //public List<ComponentLookupDto> GetComponent()
        //{
        //    //TODO: Load data from real database
        //    List<ComponentLookupDto> Components = new List<ComponentLookupDto>();
        //    Components.Add(new ComponentLookupDto { ComponentLookupId = 1, ComponentChoice = "ComponentType" });
        //    Components.Add(new ComponentLookupDto { ComponentLookupId = 2, ComponentChoice = "ComponentOrderNumber" });
        //    Components.Add(new ComponentLookupDto { ComponentLookupId = 3, ComponentChoice = "ComponentSerialNumber" });
        //    Components.Add(new ComponentLookupDto { ComponentLookupId = 4, ComponentChoice = "ComponentSupplyNumber" });
        //    return Components;
        //}
    }
}
