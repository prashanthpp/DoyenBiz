
using MahApps.Metro.Controls;

using DoyenBiz.BankingKiosk.ViewModels;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for PinValidation.xaml
    /// </summary>
    public partial class PinValidation : Flyout
    {
        public PinValidation()
        {
            InitializeComponent();
            this.DataContext = (new PinValidationViewModel());
        }
    }
}
