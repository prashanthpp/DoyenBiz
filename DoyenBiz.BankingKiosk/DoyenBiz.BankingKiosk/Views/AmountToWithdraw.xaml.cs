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
using System.Collections.ObjectModel;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.BankingKiosk.Utilities;
using DoyenBiz.BankingKiosk.ViewModels;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for AmountToWithdraw.xaml
    /// </summary>
    public partial class AmountToWithdraw : Flyout
    {
        public AmountToWithdraw()
        {
            InitializeComponent();
            this.DataContext = (new AmountToWithdrawViewModel());
        }
    }
}
