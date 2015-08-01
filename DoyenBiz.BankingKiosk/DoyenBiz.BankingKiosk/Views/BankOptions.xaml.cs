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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using DoyenBiz.BankingKiosk.Utilities;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for BankOptions.xaml
    /// </summary>
    public partial class BankOptions : MetroWindow
    {
        public BankOptions()
        {
            InitializeComponent();
        }

        private void WithdrawCash_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.WithdrawCash);
        }
    }
}
