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

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class ElectricityHomeViewModel : BaseViewModel
    {
        public string _balance;
        public string Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public ElectricityHomeViewModel()
        {
            //Make get call and set Balance
            try
            {
                string servicesUri = ConfigurationManager.AppSettings["servicesUri"].ToString();
                string kioskId = ConfigurationManager.AppSettings["kioskID"].ToString();
                string queryUri = servicesUri + "?service=CitizenServices&action=GetKioskBalance&kioskid=" + kioskId;
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
                        Balance = jOb["KioskBalance"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Balance = "Error occurred while fetching balance";
            }
        }
    }
}
