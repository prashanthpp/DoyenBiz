using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class TransactionHomeViewModel: BaseViewModel
    {
        public TransactionHomeViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(prePaidButton_Click));
            Progress += 20;

        }

        public async void prePaidButton_Click(object obj)
        {
            ToggleFlyout(0);
        }
    }
}
