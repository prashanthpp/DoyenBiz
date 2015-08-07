using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.BankingKiosk.Utilities;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class AmountToWithdrawViewModel : BaseViewModel
    {

        public AmountToWithdrawViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(btnAmount_Click));
            Progress += 80;

        }

        public async void btnAmount_Click(object obj)
        {
            var enteredAmount = obj.ToString();
            //enteredAmount = "100";
            if (obj == null || String.IsNullOrWhiteSpace(obj.ToString()))
            {
                await CurrentWindow.ShowMessageAsync("Please enter amount to withdraw", "");
            }
            else if (Int32.Parse(enteredAmount) % 100 != 0)
            {
                await CurrentWindow.ShowMessageAsync("Wrong amount entered", "Amount should be entered as multiples of 100");
            }
            else
            {
                var controller = await CurrentWindow.ShowProgressAsync("Processing your Transaction... ", "Please Wait...");
                controller.SetCancelable(true);

                bool amtWithdrawSuccessful = false;
                try
                {
                    string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                    string queryUri = servicesUri + "&action=VerifyTransaction&kioskid=101&cardno=10001&pin=1234&amount=" + enteredAmount;
                    HttpWebRequest myRequest =
                      (HttpWebRequest)WebRequest.Create(queryUri);
                    myRequest.Method = "POST";
                    myRequest.Accept = "application/json";
                    using (var resp = (HttpWebResponse)myRequest.GetResponse())
                    {
                        using (var reader = new StreamReader(resp.GetResponseStream()))
                        {
                            string text = reader.ReadToEnd();
                            var jsonDe = JsonConvert.DeserializeObject(text);
                            JObject jOb = (JObject)jsonDe;
                            string transactionID = jOb["TransactionID"].ToString();
                            amtWithdrawSuccessful = true;
                            await controller.CloseAsync();

                            //Write the service calls to get the status of the transaction
                            Transaction_InProgress(transactionID);

                            Transaction_Completed(obj);
                            if (controller.IsCanceled)
                            {
                                await controller.CloseAsync();
                                Transaction_Cancelled(obj);
                                await CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                                NavigationServiceHelper.Navigate(enteredAmount, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    amtWithdrawSuccessful = false;
                }

                if (!amtWithdrawSuccessful)
                {
                    await controller.CloseAsync();
                    if (controller.IsCanceled)
                    {
                        Transaction_Cancelled(obj);
                        await CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                        NavigationServiceHelper.Navigate(enteredAmount, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                    }
                    else
                    {
                        await CurrentWindow.ShowMessageAsync("Transaction not successful, Please try again", "Going to Home page");
                        NavigationServiceHelper.Navigate(enteredAmount, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                    }
                }
                else
                {
                    await Task.Delay(1000);
                    //this.inputPIN.Visibility = Visibility.Collapsed;
                    Progress += 20;
                    await controller.CloseAsync();
                    //this.bioAuth.Visibility = Visibility.Visible;
                }
            }
        }


        private async void Transaction_Completed(object sender)
        {
            Progress += 100;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "I Collected Cash",
                ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await CurrentWindow.ShowMessageAsync("Your Transaction is approved by  Account Holder ... Please Collect Cash", "",
                MessageDialogStyle.Affirmative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((sender as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }

        private async void Transaction_InProgress(string transactionId)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Cancel Transaction",
                ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await CurrentWindow.ShowMessageAsync("Completing two-factor Biometric Authentication...", "Verifying transaction with the Account holder...",
                MessageDialogStyle.Affirmative, mySettings);

            //do
            //{
            //    //logic to call the Verify Transaction service here... and break once we receive the final message
                
            //    await Task.Delay(1000);
            //} while (true);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((transactionId as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }

        private async void Transaction_Cancelled(object sender)
        {
            Progress += 100;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "I Collected Cash",
                ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await CurrentWindow.ShowMessageAsync("Your Transaction is approved by  Account Holder ... Please Collect Cash", "",
                MessageDialogStyle.Affirmative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((sender as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }
    }
}
