using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.CitizenKiosk.Utilities;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class PayCashViewModel : BaseViewModel
    {
        public PayCashViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(GoButton_Click));
            Progress += 60;

        }

        public async void GoButton_Click(object inputBoxValue)
        {
            bool tranSuccess = false;
            try
            {
                string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                string kioskId = ConfigurationManager.AppSettings["kioskID"].ToString();
                string queryUri = servicesUri + "?service=CitizenServices&action=RechargeMobile&kioskid=" + kioskId + "&rechargeamt=" + RechargeAmountViewModel._rechargeAmount +
                                                                            "&depositedamt=100&rechargemobilenumber=" + MobileNumberViewModel._mobileNumber;
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
                        string transactionStatus = jOb["TransactionStatus"].ToString();

                        if (transactionStatus.ToLower() == "recharge success")
                        {
                            tranSuccess = true;
                            await Task.Delay(1000);
                            Progress += 20;
                            await CurrentWindow.ShowMessageAsync("Transaction Successful...", "Please check SMS with updated balance from your service provider");
                            NavigationServiceHelper.Navigate("Success", CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
                        }
                        else
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
                await CurrentWindow.ShowMessageAsync("Transaction failed", "A credit note will be sent to your mobile number for the amount deposited, you can use the credit note to try recharging  now or later");
                NavigationServiceHelper.Navigate("Success", CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }
    }
}
