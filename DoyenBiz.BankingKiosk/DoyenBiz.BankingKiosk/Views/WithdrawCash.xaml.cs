using MahApps.Metro.Controls;
using DoyenBiz.BankingKiosk.ViewModels;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for WithdrawCash.xaml
    /// </summary>
    public partial class WithdrawCash : Flyout
    {

        public WithdrawCash()
        {
            InitializeComponent();
            this.DataContext = (new WithdrawCashViewModel());
        }

    }
}