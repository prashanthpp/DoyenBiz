using System;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class WithdrawCashViewModel : BaseViewModel
    {

        private int inputPINNumber;


        public int InputPINNumber
        {
            get { return inputPINNumber; }
            set
            {
                inputPINNumber = value;
                OnPropertyChanged("InputPINNumber");
            }
        }


        public WithdrawCashViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(cardEnteredButton_Click));
            Progress += 20;

        }

        public void cardEnteredButton_Click(object obj)
        {
            ToggleFlyout(1);
            ToggleFlyout(0);
        }

    }
}
