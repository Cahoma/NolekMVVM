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
    /// Interaction logic for CreateEquipmentPage.xaml
    /// </summary>
    public partial class CreateEquipmentPage : Page
    {
        public CreateEquipmentPage(IEquipmentCreateViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ClearAllFields();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ClearAllFields();
        }
        private void ClearAllFields()
        {
            txtImagePath.Text = String.Empty;
            txtMainNumber.Text = String.Empty;
            txtSerial.Text = String.Empty;
            dpDateCreated.Text = String.Empty;
        }
    }
}
