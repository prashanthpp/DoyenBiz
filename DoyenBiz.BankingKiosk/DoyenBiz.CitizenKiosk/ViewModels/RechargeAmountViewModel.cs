using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.CitizenKiosk.Utilities;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.Windows.Controls;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class RechargeAmountViewModel : BaseViewModel
    {

        private ICommand m_AmountButtonCommand;
        private string selectedAmount;
        public static string _rechargeAmount;
        private ICommand m_KeyButtonCommand;

        public ICommand AmountChangeCommand
        {
            get
            {
                return m_AmountButtonCommand;
            }
            set
            {
                m_AmountButtonCommand = value;
            }
        }


        public ICommand KeyButtonCommand
        {
            get
            {
                return m_KeyButtonCommand;
            }
            set
            {
                m_KeyButtonCommand = value;
            }
        }

        public string SelectedAmount
        {
            get { return selectedAmount; }
            set
            {
                selectedAmount = value;
                OnPropertyChanged("SelectedAmount");
            }
        }

        public RechargeAmountViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(btnAmount_Click));
            AmountChangeCommand = new RelayCommand(new Action<object>(btnAmountChange_Click));
            KeyButtonCommand = new RelayCommand(new Action<object>(KeyButtonCommand_Click));
            Progress += 40;

        }


        public async void btnAmountChange_Click(object amountSelected)
        {
            SelectedAmount = amountSelected.ToString();
        }
        public async void KeyButtonCommand_Click(object inputBoxValue)
        {
            if (inputBoxValue.ToString() != "backspace")
                SelectedAmount = SelectedAmount + inputBoxValue.ToString();
            else
            {
                if (!string.IsNullOrEmpty(SelectedAmount))
                {
                    SelectedAmount = SelectedAmount.Remove(SelectedAmount.Length - 1, 1);
                }
            }

        }
        public async void btnAmount_Click(object obj)
        {
            var enteredAmount = obj.ToString();
            if (obj == null || String.IsNullOrWhiteSpace(obj.ToString()))
            {
                await CurrentWindow.ShowMessageAsync("Please enter amount to recharge", "");
            }
            else if (Int32.Parse(enteredAmount) % 10 != 0)
            {
                await CurrentWindow.ShowMessageAsync("Wrong amount entered", "Amount should be entered as multiples of 10");
            }
            else
            {
                _rechargeAmount = SelectedAmount;
                ToggleFlyout(1);
                ToggleFlyout(2);
            }
        }

    }
}
