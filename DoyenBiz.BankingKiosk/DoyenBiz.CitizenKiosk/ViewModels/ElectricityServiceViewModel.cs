using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class ElectricityServiceViewModel : BaseViewModel
    {

        private ICommand m_KeyButtonCommand;
        public static string _mobileNumber;

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

        public TextBox InputBox
        {
            get;
            set;
        }

        public ElectricityServiceViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(GoButton_Click));
            KeyButtonCommand = new RelayCommand(new Action<object>(KeyButtonCommand_Click));
        }

        public async void GoButton_Click(object inputBoxValue)
        {
            var enteredNumber = string.Empty;
            if (inputBoxValue != null && inputBoxValue is TextBox)
                enteredNumber = ((TextBox)inputBoxValue).Text;
            if (String.IsNullOrWhiteSpace(enteredNumber))
            {
                await CurrentWindow.ShowMessageAsync("Please enter Mobile Number to proceed", "");
            }
            else if (enteredNumber.Length > 0 && enteredNumber.ToString().Length < 10)
            {
                await CurrentWindow.ShowMessageAsync("Please enter all the 10 digits of Mobile Number", "");
            }
            else
            {
                _mobileNumber = enteredNumber;
                ToggleFlyout(0);
                ToggleFlyout(1);
            }
        }

        public async void KeyButtonCommand_Click(object inputBoxValue)
        {
            if (inputBoxValue.ToString() != "backspace")
                InputBox.Text = InputBox.Text + inputBoxValue.ToString();
            else
            {
                if (!string.IsNullOrEmpty(InputBox.Text))
                {
                    InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 1, 1);
                }
            }

        }
    }
}
