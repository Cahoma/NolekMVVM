using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public interface IUserRepository
    {
        void Add(Model.Login model);
        bool HasChanges();
        void Remove(Model.Login model);
        Task SaveAsync();
    }
}
