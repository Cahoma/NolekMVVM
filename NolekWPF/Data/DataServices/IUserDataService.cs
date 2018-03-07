using System.Collections.Generic;
using NolekWPF.Model;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public interface IUserDataService
    {
        //List<User> GetUser();
        //User GetUserInstance(string username);
        Task<Login> GetByNameAsync(string username);
    }
}