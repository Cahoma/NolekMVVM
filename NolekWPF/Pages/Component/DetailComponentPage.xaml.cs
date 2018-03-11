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
    /// Interaction logic for DetailComponentPage.xaml
    /// </summary>
    public partial class DetailComponentPage : Page
    {
        private IComponentDetailViewModel _viewmodel;

        public DetailComponentPage(IComponentDetailViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void compList_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new ListComponentPage(_viewmodel.ComponentListViewModel));
            this.NavigationService.GoBack();
        }
    }
}
