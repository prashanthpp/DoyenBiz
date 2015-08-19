using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.CitizenKiosk.Utilities;

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

        public TextBox ServiceInputBox
        {
            get;
            set;
        }

        public TextBox FocusBox
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
            if (InputBox as TextBox != null)
                enteredNumber = ((TextBox)InputBox).Text;
            if (String.IsNullOrWhiteSpace(ServiceInputBox.Text.ToString()))
            {
                await CurrentWindow.ShowMessageAsync("Please enter Mobile Number to proceed", "");
            }
            else if (ServiceInputBox.Text.ToString().Length > 0 && ServiceInputBox.Text.ToString().ToString().Length < 10)
            {
                await CurrentWindow.ShowMessageAsync("Please enter all the 10 digits of Mobile Number", "");
            }
            else
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Try Again",
                    ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
                };

                await Task.Delay(2000);
                MessageDialogResult result = await CurrentWindow.ShowMessageAsync("ERO and/or Service Number could not be located...", "",
                    MessageDialogStyle.Affirmative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    NavigationServiceHelper.Navigate((inputBoxValue as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                }
            }
        }

        public async void KeyButtonCommand_Click(object inputBoxValue)
        {
            if (inputBoxValue.ToString() != "backspace" && inputBoxValue.ToString() != "space")
                FocusBox.Text = FocusBox.Text + inputBoxValue.ToString();
            else if(inputBoxValue.ToString() == "backspace")
            {
                if (!string.IsNullOrEmpty(InputBox.Text))
                {
                    FocusBox.Text = FocusBox.Text.Remove(FocusBox.Text.Length - 1, 1);
                }
            }
            else if (inputBoxValue.ToString() == "space")
            {

                FocusBox.Text = FocusBox.Text + " ";
            }
        }
    }
}
