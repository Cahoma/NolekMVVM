using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NolekWPF.ViewModels.Main;

namespace NolekWPF.Pages.Main
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        private IUserCreateViewModel _viewmodel;
        public CreateUser(IUserCreateViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewmodel.CurrentUser.RoleId == 3)
            {
                this.NavigationService.Navigate(new UpdateUserAdmin(_viewmodel.UserUpdateAdminViewModel));
            }
        }

    }
}
