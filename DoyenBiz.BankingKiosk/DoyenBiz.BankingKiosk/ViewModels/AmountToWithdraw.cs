using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.BankingKiosk.Utilities;
using System.Windows.Controls;

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
            var controller = await CurrentWindow.ShowProgressAsync("Processing your Transaction ... please wait", "Completing two-factor Biometric Authentication ... \nVerifying transaction with account holder ...");
            controller.SetCancelable(true);

            double i = 0.0;
            while (i < 6.0)
            {
                double val = (i / 100.0) * 20.0;
                controller.SetProgress(val);
                if (controller.IsCanceled)
                    break; //canceled progressdialog auto closes.

                i += 1.0;

                await Task.Delay(1000);
            }

            await controller.CloseAsync();

            if (controller.IsCanceled)
            {
                await CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                NavigationServiceHelper.Navigate(obj as string, CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
            else
            {
                Transaction_Completed(obj);
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

    }
}
