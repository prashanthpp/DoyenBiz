using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using DoyenBiz.BankingKiosk.Views;

namespace DoyenBiz.BankingKiosk.Utilities
{
    public static class NavigationServiceHelper
    {

        public enum TargetWindow
        {
            BankOptions
        }

        public static void Navigate(Button element, MetroWindow currentWindow, TargetWindow targetWindow)
        {
            var tagInfo = (element as Button).Tag;
            var tWindow = GetTargetWindowRef(targetWindow);
            tWindow.Show();
            currentWindow.Hide();
        }

        private static MetroWindow GetTargetWindowRef(TargetWindow tWindow)
        {
            switch (tWindow)
            {
                case TargetWindow.BankOptions:
                    return new BankOptions();
                   
                default:
                    throw new System.NotImplementedException("Invalid target window!");

            }

        }
    }
}
