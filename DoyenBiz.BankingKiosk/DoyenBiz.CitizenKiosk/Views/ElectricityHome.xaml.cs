using DoyenBiz.CitizenKiosk.ViewModels;
using MahApps.Metro.Controls;
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

namespace DoyenBiz.CitizenKiosk.Views
{
    /// <summary>
    /// Interaction logic for ElectricityHome.xaml
    /// </summary>
    public partial class ElectricityHome : MetroWindow
    {
        public ElectricityHome()
        {
            InitializeComponent();
            this.DataContext = (new ElectricityHomeViewModel());
            BaseViewModel.CurrentWindow = this;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            ((BaseViewModel)this.DataContext).ToggleFlyout(0);
        }
    }
}
