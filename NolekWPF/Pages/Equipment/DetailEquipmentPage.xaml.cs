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
    /// Interaction logic for DetailEquipmentPage.xaml
    /// </summary>
    public partial class DetailEquipmentPage : Page
    {
        public DetailEquipmentPage(IEquipmentDetailViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }
    }
}
