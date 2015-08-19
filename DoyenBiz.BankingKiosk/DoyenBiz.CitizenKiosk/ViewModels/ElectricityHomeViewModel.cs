using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class ElectricityHomeViewModel : BaseViewModel
    {

        public ElectricityHomeViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(GoButton_Click));
        }


        public void GoButton_Click(object inputBoxValue)
        {
            ToggleFlyout(0);
        }
    }
}
