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
using MahApps.Metro.Controls;
using DoyenBiz.CitizenKiosk.ViewModels;
using DoyenBiz.CitizenKiosk.Utilities;

namespace DoyenBiz.CitizenKiosk.Views
{
    /// <summary>
    /// Interaction logic for TransactionHome.xaml
    /// </summary>
    public partial class TransactionHome : MetroWindow
    {
        public static string SelectedProvider;
        public TransactionHome()
        {
            InitializeComponent();
            TransactionHomeViewModel.CurrentWindow = this;
            imgProvider.Source = new BitmapImage(new Uri(SelectedProvider, UriKind.Relative));
        }


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.HomePage);
        }
    }
}
