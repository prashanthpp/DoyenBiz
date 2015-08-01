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
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.BankingKiosk.Views;
using DoyenBiz.BankingKiosk.Utilities;

namespace DoyenBiz.BankingKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bank_click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.BankOptions);
        }
    }
}
