using NolekWPF.DataAccess;
using NolekWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class UserLookupDataService : IUserLookupDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public UserLookupDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator; //context is kept alive throughout the application lifetime
        }

        public async Task<IEnumerable<UserLookup>> GetUserLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Logins.AsNoTracking().Select(f => new UserLookup
                {
                    Username = f.Username,
                    Password = f.Password,
                    Role = f.Role

                }).ToListAsync();
            }
        }
    }
}
