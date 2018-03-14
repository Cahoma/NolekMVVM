using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using NolekWPF.Wrappers;
using System.Collections.ObjectModel;

namespace NolekWPF.Data.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public EquipmentRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void Add(Model.Equipment equipment)
        {
            _context.Equipments.Add(equipment); //call insert to add new equipement to table
        }
        

        public async Task<Model.Equipment> GetByIdAsync(int equipId)
        {
            return await _context.Equipments.SingleAsync(f => f.EquipmentId == equipId); //return equipement with the id
        }

        public async Task<IEnumerable<EquipmentTypeDto>> GetEquipmentTypesAsync()
        {
            return await _context.EquipmentTypes.Select(t => new EquipmentTypeDto()
            {
                EquipmentTypeID = t.EquipmentTypeID,
                EquipmentTypeDescription = t.EquipmentTypeDescription,
                EquipmentTypeName = t.EquipmentTypeName

            }).ToListAsync();
        }
        public async Task<IEnumerable<EquipmentConfigurationDto>> GetEquipmentConfigurationsAsync()
        {
            return await _context.EquipmentConfigurations.Select(c => new EquipmentConfigurationDto()
            {
                EquipmentConfigurationId = c.EquipmentConfigurationId,
                EquipmentConfigurationDescription = c.EquipmentConfigurationDescription

            }).ToListAsync();
        }
        public async Task<IEnumerable<EquipmentCategoryDto>> GetEquipmentCategoriesAsync()
        {
            return await _context.EquipmentCategories.Select(c => new EquipmentCategoryDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToListAsync();
        }
        public async Task<IEnumerable<ContactPersonDto>> GetEquipmentContactPersonsAsync()
        {
            return await _context.ContactPersons.Select(c => new ContactPersonDto()
            {
                FirstName = c.ContactPersonFirstName,
                LastName = c.ContactPersonLastName,
                Email = c.ContactPersonEmail,
                Telephone = c.ContactPersonTlf
            }).ToListAsync();
        }

        public List<EquipmentComponent> GetEquipmentComponents(int equipmentId)
        {
            return _context.EquipmentComponents.Where(c => c.EquipmentID == equipmentId).ToList();
            
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); //return true if current equipement has changes
        }

        public void Remove(Model.Equipment model)
        {
            _context.Equipments.Remove(model); //delete equipment from the db
        }

        public void RemoveEquipmentComponent(EquipmentComponent model)
        {
            var ec = _context.EquipmentComponents.FirstOrDefault( c => c.ComponentName == model.ComponentName);
            if (ec != null)
            {
                _context.EquipmentComponents.Remove(ec);
            }
            
        }

        public void Update(Model.Equipment model)
        {
            var Entity = _context.Equipments.Find(model.EquipmentId);
            if (Entity == null)
            {
                return;
            }
            //_context.Entry(model).CurrentValues.SetValues(model);
        }

        public void UpdateComponents(ComponentDto model, int equipmentId)
        {
            //add new relations between component and equipment
            //Entity is not null if relation already exists
            //var Entity = _context.EquipmentComponents.Find(model.ComponentId, equipmentId);

            //create new relation
            
                _context.EquipmentComponents.Add(new EquipmentComponent()
                {
                    ComponentID = model.ComponentId,
                    EquipmentID = equipmentId,
                    ComponentName = model.ComponentName
                });
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
