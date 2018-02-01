using NolekWPF.Pages.Equipment;
using NolekWPF.Pages.Component;
using NolekWPF.Pages.Customer;
using NolekWPF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NolekWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel; //assign injected view model to local property
            DataContext = viewModel; //set the data context for the view to the viewmodel
            Loaded += MainWindow_Loaded; //event when the window first loads
            
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void mnuEquipList_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new ListEquipmentPage(_viewModel.EquipmentListViewModel));
        }
        private void mnuEquipCreate_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new CreateEquipmentPage(_viewModel.EquipmentCreateViewModel));
        }
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void compList_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new ListComponentPage(_viewModel.ComponentListViewModel));
        }

        private void mnuCreateComp_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new CreateComponentPage(_viewModel.ComponentCreateViewModel));
        }

        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MenuVisibility = "Collapsed";
            _viewModel.Visibility = "Visible";
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void mnu_addRemoveComp_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new AddRemoveComponentPage(_viewModel.AddRemoveComponentViewModel));
        }

        

        private void mnu_listCust_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new ListCustomerPage(_viewModel.CustomerListViewModel));
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HarvestPassword += (p, args) =>
    args.Password = txtPassword.Password;
        }

        private void mnu_addRemoveEquip_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new AddRemoveEquipmentToFromCustomerPage(_viewModel.AddRemoveEquipmentToFromCustomerViewModel));
        }
    }
}
