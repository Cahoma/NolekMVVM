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
    /// Interaction logic for CreateComponentPage.xaml
    /// </summary>
    public partial class CreateComponentPage : Page
    {
        public CreateComponentPage(IComponentCreateViewModel viewmodel)
        {
            
            InitializeComponent();
            DataContext = viewmodel;
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
    }
}
