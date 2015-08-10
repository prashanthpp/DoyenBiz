using System;
using DoyenBiz.BankingKiosk.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using System.Net;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class PinValidationViewModel:BaseViewModel
    { 
    public PinValidationViewModel()
    {
        ButtonCommand = new RelayCommand(new Action<object>(validatePINButton_Click));
            Progress += 40;

        }

   
        public async void validatePINButton_Click(object inputBox)
        {
            var enteredPIN = string.Empty;
            if (inputBox != null && inputBox is PasswordBox)
                enteredPIN = ((PasswordBox)inputBox).Password;
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
                    await Task.Delay(1000);
                    string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                    string kioskID = ConfigurationManager.AppSettings["kioskID"].ToString();
                    string cardNo = ConfigurationManager.AppSettings["cardNumber"].ToString();
                    string queryUri = string.Format("{0}?service=BankingServices&action=VerifyPIN&kioskid={1}&cardno={3}&pin={2}", servicesUri,kioskID, enteredPIN,cardNo);
                    HttpWebRequest myRequest =
                      (HttpWebRequest)WebRequest.Create(queryUri);
                    myRequest.Method = "POST";
                    myRequest.Accept = "application/json";
                    using (var resp = (HttpWebResponse)myRequest.GetResponse())
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            pinValidateSuccessful = true;
                            await controller.CloseAsync();
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                    {
                        pinValidateSuccessful = false;
                    }
                }
                //pinValidateSuccessful = true; //remove this line of code once the VerifyPIN method is fixed
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
                    //this.bioAuth.Visibility = Visibility.Visible;
                }
            }

        }
    }

}
