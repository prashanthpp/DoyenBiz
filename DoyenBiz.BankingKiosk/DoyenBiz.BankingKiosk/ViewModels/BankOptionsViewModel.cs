using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MahApps.Metro.Controls;

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
