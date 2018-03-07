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
    public class UserDataService : IUserDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public UserDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Model.Login> GetByNameAsync(string username)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Logins.AsNoTracking().SingleAsync(f => f.Username == username);
            }
        }

        //public List<User> GetUser()
        //{
        //    //TODO: Load data from real database
        //    List<User> Users = new List<User>();
        //    Users.Add(new User { Username = "UserAdmin", Password = "123", Role = "Secretary", LoggedIn = false});
        //    Users.Add(new User { Username = "UserRegular", Password = "123", Role = "Technician", LoggedIn = false });
        //    return Users;
        //}

        //public User GetUserInstance(string username)
        //{
        //    List<User> Users = GetUser();
        //    User user = new User();
        //    foreach (var u in Users)
        //    {
        //        if (u.Username == username)
        //        {
        //            u.LoggedIn = true;
        //            user.Username = u.Username;
        //            user.Role = u.Role;
        //            user.LoggedIn = u.LoggedIn;
        //            break;
        //        }
        //    }
        //    return user;
        //}
    }
}
