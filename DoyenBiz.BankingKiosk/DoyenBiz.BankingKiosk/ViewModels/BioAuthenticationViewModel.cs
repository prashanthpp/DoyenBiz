using System;
namespace DoyenBiz.BankingKiosk.ViewModels
{
    class BioAuthenticationViewModel : BaseViewModel
    {
        public BioAuthenticationViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(thumbprintButton_Click));
            Progress += 60;

        }

        public async void thumbprintButton_Click(object obj)
        {
            ToggleFlyout(3);
            ToggleFlyout(2);
        }
    }
}