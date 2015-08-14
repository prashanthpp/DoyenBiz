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
using System.Windows.Input;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    class AmountToWithdrawViewModel : BaseViewModel
    {

        private ICommand m_AmountButtonCommand;
        private string selectedAmount;
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

        public AmountToWithdrawViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(btnAmount_Click));
            AmountChangeCommand = new RelayCommand(new Action<object>(btnAmountChange_Click));
            KeyButtonCommand = new RelayCommand(new Action<object>(KeyButtonCommand_Click));
            Progress += 80;

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
                await Task.Delay(500);

                bool tranSuccess = false;
                bool closeController = true;
                try
                {
                    string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();

                    string imageToSend = ImageConverter.EncodeInHTML(ImageConverter.ConvertToBase64(ConfigurationManager.AppSettings["imageToSendPath"]));
                    string cardNo = ConfigurationManager.AppSettings["cardNumber"].ToString();
                    string kioskId = ConfigurationManager.AppSettings["kioskID"].ToString();

                    string queryUri = "service=BankingServices&action=VerifyTransaction&kioskid="+kioskId+"&cardno="+cardNo +"&pin=1234&amount=" + enteredAmount + "&image=" + imageToSend;
                    HttpWebRequest myRequest =
                      (HttpWebRequest)WebRequest.Create(servicesUri);
                   
                    myRequest.Method = "POST";
                    myRequest.ContentType = "application/x-www-form-urlencoded";

                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byte1 = encoding.GetBytes(queryUri);
                    myRequest.ContentLength = byte1.Length;

                    Stream newStream = myRequest.GetRequestStream();

                    newStream.Write(byte1, 0, byte1.Length);

                    using (var resp = (HttpWebResponse)myRequest.GetResponse())
                    {
                        using (var reader = new StreamReader(resp.GetResponseStream()))
                        {
                            string text = reader.ReadToEnd();
                            var jsonDe = JsonConvert.DeserializeObject(text);
                            JObject jOb = (JObject)jsonDe;
                            string transactionID = jOb["TransactionID"].ToString();
                            await controller.CloseAsync();
                            closeController = false;

                            //Write the service calls to get the status of the transaction
                            tranSuccess = await Transaction_InProgress(transactionID);

                            if(tranSuccess)
                                await Transaction_Completed(obj);
                            else
                            {
                                await CurrentWindow.ShowMessageAsync("Transaction Failed", "Going to Home page..");
                                NavigationServiceHelper.Navigate(enteredAmount, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                            }

                            if (controller.IsCanceled)
                            {
                                tranSuccess = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    tranSuccess = false;
                }

                if (!tranSuccess)
                {
                    if(closeController)
                        await controller.CloseAsync();

                    if (controller.IsCanceled)
                    {
                        await Transaction_Cancelled(obj);
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
                    Progress += 20;
                }
            }
        }


        private async Task Transaction_Completed(object sender)
        {
            Progress += 100;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "I Collected Cash",
                ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await CurrentWindow.ShowMessageAsync("Your Transaction is Approved...", "Please Collect Cash...",
                MessageDialogStyle.Affirmative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((sender as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }

        private async Task<bool> Transaction_InProgress(string transactionId)
        {
            var controllerInProgress = await CurrentWindow.ShowProgressAsync("Processing your Transaction... Please wait", "Completing two-factor Biometric Authentication...");
            controllerInProgress.SetCancelable(true);
            await Task.Delay(2000);

            string transactionStatus;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                //logic to call the Verify Transaction service here... and break once we receive the success message
                try
                {
                    string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                    string kioskId = ConfigurationManager.AppSettings["kioskID"].ToString();
                    string queryUri = servicesUri + "?service=BankingServices&action=GetTransactionStatus&kioskid="+kioskId+"&transactionid=" + transactionId;
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
                            transactionStatus = jOb["TransactionStatus"].ToString();

                            if (transactionStatus.ToLower() == "approved")
                            {
                                await controllerInProgress.CloseAsync();
                                stopwatch.Stop();
                                return true;
                            }
                            else if(transactionStatus.ToLower() == "biocheckwip")
                            {
                                if( stopwatch.Elapsed.TotalSeconds > 10)
                                {
                                    await controllerInProgress.CloseAsync();
                                    stopwatch.Stop();
                                    await Transaction_Cancelled(transactionId);
                                }
                                await Task.Delay(1000);
                            }
                            else if (transactionStatus.ToUpper() == "CHECKINGWITHACCTHOLDER")
                            {
                                await controllerInProgress.CloseAsync();
                                stopwatch.Stop();
                                //stopwatch.Restart();
                                controllerInProgress = await CurrentWindow.ShowProgressAsync("Processing your Transaction... Please wait", "Verifying transaction with the Account holder... this may take some time...");
                                //if (stopwatch.Elapsed.TotalSeconds > 20)
                                //{
                                //    await Transaction_Cancelled(transactionId, "NORESPONSEFROMACCTHOLDER");
                                //}
                            }
                            else if (transactionStatus.ToUpper() == "NORESPONSEFROMACCTHOLDER")
                            {
                                await controllerInProgress.CloseAsync();
                                stopwatch.Stop();
                                await Transaction_Cancelled(transactionId, transactionStatus.ToUpper());
                            }
                            else if (transactionStatus.ToUpper() == "ACCTHOLDERREJECTEDTR")
                            {
                                await controllerInProgress.CloseAsync();
                                stopwatch.Stop();
                                await Transaction_Cancelled(transactionId, transactionStatus.ToUpper());
                            }

                            if (controllerInProgress.IsCanceled)
                            {
                                await controllerInProgress.CloseAsync();
                                stopwatch.Stop();
                                await Transaction_Cancelled(transactionId);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            } while (transactionStatus.ToLower() != "approved");
            stopwatch.Stop();
            return false;
        }

        private async Task Transaction_Cancelled(object sender,string cancelReason = "GENERAL")
        {
            string buttonText = "OK";
            string DialogueMainText = "Your transaction could not be completed...";
            string dialogueSubText = "";
            switch (cancelReason)
            {
                case "NORESPONSEFROMACCTHOLDER" : 
                    buttonText = "Return Home";
                    DialogueMainText = "Your transaction could not be completed...";
                    dialogueSubText = "Account Holder rejected the transaction or there was no response from Account Holder...";
                    break;

                case "ACCTHOLDERREJECTEDTR":
                    buttonText = "Return Home";
                    DialogueMainText = "Your transaction could not be completed...";
                    dialogueSubText = "Account Holder rejected the transaction or there was no response from Account Holder...";
                    break;

                case "INSUFFICIENTFUNDS": 
                    buttonText = "Return Home";
                    DialogueMainText = "Your transaction could not be completed...";
                    dialogueSubText = "Insufficient funds in Account...";
                    break;

                default: 
                    buttonText = "OK";
                    DialogueMainText = "Your Transaction is Cancelled...";
                    dialogueSubText="";
                    break;
            }

            Progress += 100;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = buttonText,
                ColorScheme = CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await CurrentWindow.ShowMessageAsync(DialogueMainText,dialogueSubText,
                MessageDialogStyle.Affirmative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((sender as string), CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }
    }
}
