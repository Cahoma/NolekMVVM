using NolekWPF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Data.Entity;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public ComponentRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void Add(Component component)
        {
            _context.Components.Add(component); //call insert to add new equipement to table
        }

        public async Task<Component> GetByIdAsync(int compId)
        {
            return await _context.Components.SingleAsync(f => f.ComponentId == compId); //return equipement with the id
        }


        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); //return true if current equipement has changes
        }

        public void Remove(Component model)
        {
            _context.Components.Remove(model); //delete equipment from the db
        }

        public async Task<IEnumerable<ComponentLookupDto>> GetComponentChoiceAsync()
        {
            return await _context.ComponentChoices.Select(c => new ComponentLookupDto()
            {
                ComponentLookupId = c.ChoiceId,
                ComponentChoice = c.CompChoice
            }).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
