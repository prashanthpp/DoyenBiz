﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.BankingKiosk.Utilities;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for WithdrawCash.xaml
    /// </summary>
    public partial class WithdrawCash : Flyout, INotifyPropertyChanged
    {

        private int m_progress;
        private int inputPINNumber;

        public int Progress
        {
            get { return m_progress; }
            set
            {
                m_progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public int InputPINNumber
        {
            get { return inputPINNumber; }
            set
            {
                inputPINNumber = value;
                OnPropertyChanged("InputPINNumber");
            }
        }

        public ObservableCollection<string> Steps
        {
            get;
            set;
        }

        public WithdrawCash()
        {
            InitializeComponent();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            Steps = new ObservableCollection<string>();
            Steps.Add("Swipe Card");
            Steps.Add("Enter Pin");
            Steps.Add("Biometric Authentication");
            Steps.Add("Amount to Withdraw");
            Steps.Add("Take Cash");
            this.DataContext = this;
            Progress += 20;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void validatePINButton_Click(object sender, RoutedEventArgs e)
        {
            var controller = await BankOptions.CurrentWindow.ShowProgressAsync("Validating PIN Number ", "Please Wait...");
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
                await BankOptions.CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                NavigationServiceHelper.Navigate((sender as Button), BankOptions.CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
            else
            {
                this.inputPIN.Visibility = Visibility.Collapsed;
                Progress += 20;
                this.bioAuth.Visibility = Visibility.Visible;
            }
        }

        private void cardEnteredButton_Click(object sender, RoutedEventArgs e)
        {
            this.swipeCard.Visibility = Visibility.Collapsed;
            Progress += 20;
            this.inputPIN.Visibility = Visibility.Visible;
        }

        private void thumbprintButton_Click(object sender, RoutedEventArgs e)
        {
            this.bioAuth.Visibility = Visibility.Collapsed;
            Progress += 20;
            this.amountToWithdraw.Visibility = Visibility.Visible;
            
        }

        private async void btnAmount_Click(object sender, RoutedEventArgs e)
        {
            var controller = await BankOptions.CurrentWindow.ShowProgressAsync("Processing your Transaction ... please wait", "Completing two-factor Biometric Authentication ... \nVerifying transaction with account holder ...");
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
                await BankOptions.CurrentWindow.ShowMessageAsync("Transaction Cancelled", "Going to Home page..");
                NavigationServiceHelper.Navigate((sender as Button), BankOptions.CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
            else
            {
                Transaction_Completed(sender, e);
            }
        }


        private async void Transaction_Completed(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "I Collected Cash",
                ColorScheme = BankOptions.CurrentWindow.MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await BankOptions.CurrentWindow.ShowMessageAsync("Your Transaction is approved by  Account Holder ... Please Collect Cash", "",
                MessageDialogStyle.Affirmative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                NavigationServiceHelper.Navigate((sender as Button), BankOptions.CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
            }
        }
    }
}