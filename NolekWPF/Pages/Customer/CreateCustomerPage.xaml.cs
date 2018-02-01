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
using NolekWPF.ViewModels.Customers;
using System.Collections.ObjectModel;

namespace NolekWPF.Pages.Customer
{
    /// <summary>
    /// Interaction logic for CreateCustomerPage.xaml
    /// </summary>
    public partial class CreateCustomerPage : Page
    {
        private ICustomerCreateViewModel _viewmodel;
        public CreateCustomerPage(ICustomerCreateViewModel viewmodel)
        {
            InitializeComponent();
            _viewmodel = viewmodel;
            DataContext = _viewmodel;
        }
        private void dt_equipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;

            List<Model.Equipment> selectedObjects = selected.OfType<Model.Equipment>().ToList();

            _viewmodel.SelectedEquipments = selectedObjects;
        }
    }
}
