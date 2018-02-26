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

            //if (_context.Logins.Contains(model))
            //{
            //    _context.Logins.Remove(model);
            //}
            //if (!_context.Logins.Contains(model))
            //{
            //    _context.Logins.Attach(model);
            //}
            //_context.Logins.Remove(model);

            //var l = _context.Logins.FirstOrDefault(lo => lo.Username == model.Username);
            //if (l != null)
            //{
            var y = _context.Logins.Where(u => u.Username.Equals(model.Username)).FirstOrDefault();
            _context.Logins.Remove(y); //delete equipment from the db
            //}

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
