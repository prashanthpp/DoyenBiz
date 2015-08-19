using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class PayCashViewModel : BaseViewModel
    {
        public PayCashViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(GoButton_Click));
            Progress += 20;

        }

        public async void GoButton_Click(object inputBoxValue)
        {
        }
    }
}
