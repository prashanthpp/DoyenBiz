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
using DoyenBiz.CitizenKiosk.Views;

namespace DoyenBiz.CitizenKiosk.Utilities
{
    public static class NavigationServiceHelper
    {

        public enum TargetWindow
        {
            MobileServices,
            TransactionHome
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
                case TargetWindow.MobileServices:
                    return new MobileServices();
                case TargetWindow.TransactionHome:
                    return new TransactionHome();
                default:
                    throw new System.NotImplementedException("Invalid target window!");

            }

        }
    }
}
