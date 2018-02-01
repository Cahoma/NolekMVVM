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
    /// Interaction logic for ListComponentPage.xaml
    /// </summary>
    public partial class ListComponentPage : Page
    {
        private IComponentListViewModel _viewmodel;
        public ListComponentPage(IComponentListViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InitializeComponent();
            DataContext = _viewmodel;
        }
        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                this.NavigationService.Navigate(new DetailComponentPage(_viewmodel.ComponentDetailViewModel));
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            this.NavigationService.Navigate(new DetailComponentPage(_viewmodel.ComponentDetailViewModel));
        }
    }
}
