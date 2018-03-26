using NolekWPF.ViewModels.Component;
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

namespace NolekWPF.Pages.Component
{
    /// <summary>
    /// Interaction logic for GeneralComponentPage.xaml
    /// </summary>
    public partial class GeneralComponentPage : Page
    {
        private IComponentGeneralViewModel _viewmodel;
        public GeneralComponentPage(IComponentGeneralViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InitializeComponent();
            DataContext = _viewmodel;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            //this.NavigationService.Navigate(new DetailComponentPage(_viewmodel.ComponentDetailViewModel));
        }

        private void ClearAllFields()
        {
            txtName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtOrderNo.Text = String.Empty;
            txtSerialNo.Text = "";
            txtSupplyNo.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ClearAllFields();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ClearAllFields();
        }

        private void btnFullList_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListComponentPage(_viewmodel.ComponentListViewModel));
        }
    }
}
