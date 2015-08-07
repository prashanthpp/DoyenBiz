using MahApps.Metro.Controls;
using DoyenBiz.BankingKiosk.ViewModels;

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
            BankOptionsViewModel.CurrentWindow = this;
        }

    }
}
