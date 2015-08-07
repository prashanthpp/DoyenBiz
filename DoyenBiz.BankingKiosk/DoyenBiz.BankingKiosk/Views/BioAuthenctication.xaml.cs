using MahApps.Metro.Controls;
using DoyenBiz.BankingKiosk.ViewModels;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for BioAuthenctication.xaml
    /// </summary>
    public partial class BioAuthenctication :  Flyout
    {
        public BioAuthenctication()
        {
            InitializeComponent();
            this.DataContext = (new BioAuthenticationViewModel());
        }
    }
}
