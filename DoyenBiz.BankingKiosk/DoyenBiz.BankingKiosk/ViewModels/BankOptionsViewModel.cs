using System;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class BankOptionsViewModel: BaseViewModel
    {

        
        public BankOptionsViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(WithdrawCash_Click));
            
        }

        public void WithdrawCash_Click(object obj)
        {
            ToggleFlyout(0);
        }

    }
}
