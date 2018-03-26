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
        ICommand UpdateUserCommand { get; }
        Login NewUser { get; }
        Login CurrentUser { get; }
        UserLookup SelectedUser { get; set; }

        ObservableCollection<UserLookup> Users { get; }
        Task LoadAsync();
        Task LoadRolesAsync();

        //IUserCreateViewModel UserCreateViewModel { get; }
    }
}
