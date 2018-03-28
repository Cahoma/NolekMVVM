using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Main
{
    public interface IUserCheckLoginViewModel
    {
        ICommand CheckLoginCommand { get; }
        bool Authenticated { get; }
        string Direction { get; }
        IUserCreateViewModel UserCreateViewModel { get; }
        IUserUpdateViewModel UserUpdateViewModel { get; }
    }
}
