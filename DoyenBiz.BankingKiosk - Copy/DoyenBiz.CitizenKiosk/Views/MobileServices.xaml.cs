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
using DoyenBiz.CitizenKiosk.Utilities;

namespace DoyenBiz.CitizenKiosk
{
    /// <summary>
    /// Interaction logic for MobileServices.xaml
    /// </summary>
    public partial class MobileServices : MetroWindow
    {
        public MobileServices()
        {
            InitializeComponent();
        }

         private void provider_click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.TransactionHome);
        }
    }
}
