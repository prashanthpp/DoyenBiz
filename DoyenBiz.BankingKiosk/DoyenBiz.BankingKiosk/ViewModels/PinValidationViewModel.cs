using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using DoyenBiz.BankingKiosk.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using System.Net;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class PinValidationViewModel:BaseViewModel
    { 
    public PinValidationViewModel()
    {
        ButtonCommand = new RelayCommand(new Action<object>(validatePINButton_Click));
            Progress += 40;

        }

        public async void validatePINButton_Click(object password)
        {

            var enteredPIN = password.ToString();
            //enteredPIN = "1234";
            if (String.IsNullOrWhiteSpace(enteredPIN))
            {
                await CurrentWindow.ShowMessageAsync("Please enter PIN to proceed", "");
            }
            else if (enteredPIN.Length > 0 && enteredPIN.ToString().Length < 4)
            {
                await CurrentWindow.ShowMessageAsync("Please enter all the 4 digits of PIN","");
            }
            else
            {
                var controller = await CurrentWindow.ShowProgressAsync("Validating PIN Number ", "Please Wait...");
                controller.SetCancelable(true);

                bool pinValidateSuccessful = false;
                try
                {
                    string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                    string queryUri = servicesUri + "&action=VerifyPIN&kioskid=1011&cardno=10001&pin=" + enteredPIN;
                    HttpWebRequest myRequest =
                      (HttpWebRequest)WebRequest.Create(queryUri);
                    myRequest.Method = "POST";
                    myRequest.Accept = "application/json";
                    using (var resp = (HttpWebResponse)myRequest.GetResponse())
                    {
                        pinValidateSuccessful = true;
                        await controller.CloseAsync();
                        //////For PIN, check if the status is 200. If yes, then the validation is successful.
                        ////// Waiting for working service method.
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                    {
                        pinValidateSuccessful = false;
                    }
                }

                if (!pinValidateSuccessful)
                {
                    await controller.CloseAsync();
                    if (controller.IsCanceled)
                    {
                        await CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                        NavigationServiceHelper.Navigate(enteredPIN, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                    }
                    else
                    {
                        await CurrentWindow.ShowMessageAsync("Wrong PIN entered", "Going to Home page");
                        NavigationServiceHelper.Navigate(enteredPIN, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                    }
                }
                else
                {
                    //this.inputPIN.Visibility = Visibility.Collapsed;
                    Progress += 20;
                    ToggleFlyout(2);
                    await controller.CloseAsync();
                    //this.bioAuth.Visibility = Visibility.Visible;
                }
            }

        }
    }

}
