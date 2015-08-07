using MahApps.Metro.Controls;
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
