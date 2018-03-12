using NolekWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Main
{
    public interface IUserUpdateViewModel
    {
        Login Login { get; }
        ICommand ChangeCommand { get; }
        Task LoadAsync(int loginId);
    }
}
