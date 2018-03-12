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
    public class UserRepository: IUserRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public UserRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void Add(Login model)
        {
            _context.Logins.Add(model); //call insert to add new equipement to table
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); //return true if current equipement has changes
        }

        public void Remove(Login model)
        {
            _context.Logins.Remove(model); //delete equipment from the db
        }

        public Model.Login GetById(int loginId)
        {
            return _context.Logins.Single(f => f.LoginId == loginId); //return equipement with the id
        }

        public async Task<IEnumerable<LoginRoleDto>> GetLoginRoleAsync()
        {
            return await _context.LoginRoles.Select(c => new LoginRoleDto()
            {
                RoleId = c.RoleId,
                Role = c.Role
            }).ToListAsync();
        }

        public async Task<Login> GetByIdAsync(int loginId)
        {
            return await _context.Logins.SingleAsync(f => f.LoginId == loginId); //return equipement with the id
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
