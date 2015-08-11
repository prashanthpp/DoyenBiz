
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
            var viewModel = (new PinValidationViewModel());
            viewModel.InputBox = this.inputPINBox;
            this.DataContext = viewModel;
            inputPINBox.Focus();
        }

    }
}
