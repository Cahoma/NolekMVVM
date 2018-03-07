using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Wrappers;
using System.Collections.ObjectModel;

namespace NolekWPF.ViewModels.Main
{
    public interface IUserCreateViewModel
    {
        ICommand CreateUserCommand { get; }
        Login NewUser { get; }
        UserLookup SelectedUser { get; set; }

        ObservableCollection<UserLookup> Users { get; }
        Task LoadAsync();
    }
}
