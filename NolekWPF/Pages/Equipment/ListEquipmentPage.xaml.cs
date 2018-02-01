using NolekWPF.Equipment.ViewModels;
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

namespace NolekWPF.Pages.Equipment
{
    /// <summary>
    /// Interaction logic for ListEquipmentPage.xaml
    /// </summary>
    public partial class ListEquipmentPage : Page
    {
        private IEquipmentListViewModel _viewmodel;
        public ListEquipmentPage(IEquipmentListViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InitializeComponent();
            DataContext = viewmodel;
        }
        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null && _viewmodel.CurrentUser.Role == "Secretary")
            {
                this.NavigationService.Navigate(new DetailEquipmentPage(_viewmodel.EquipmentDetailViewModel));
            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_viewmodel.CurrentUser.Role == "Secretary")
            {
                this.NavigationService.Navigate(new DetailEquipmentPage(_viewmodel.EquipmentDetailViewModel));
            }        
        }    

    }
}
