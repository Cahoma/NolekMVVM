using NolekWPF.ViewModels.Main;
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

namespace NolekWPF.Pages.Main
{
    /// <summary>
    /// Interaction logic for CheckUser.xaml
    /// </summary>
    public partial class CheckUser : Page
    {
        private IUserCheckLoginViewModel _viewmodel;

        public CheckUser(IUserCheckLoginViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            DataContext = viewmodel;
            InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            if (_viewmodel.Authenticated == true)
            {
                this.NavigationService.Navigate(new CreateUser(_viewmodel.UserCreateViewModel));
            }
        }
    }


}
