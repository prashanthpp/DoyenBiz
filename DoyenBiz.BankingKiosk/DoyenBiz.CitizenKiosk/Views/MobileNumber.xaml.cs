using DoyenBiz.CitizenKiosk.Utilities;
using DoyenBiz.CitizenKiosk.ViewModels;
using MahApps.Metro.Controls;
using System;
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

namespace DoyenBiz.CitizenKiosk.Views
{
    /// <summary>
    /// Interaction logic for MobileNumber.xaml
    /// </summary>
    public partial class MobileNumber : Flyout
    {
        public MobileNumber()
        {
            InitializeComponent();
            var viewModel = (new MobileNumberViewModel());
            viewModel.InputBox = this.inputMobileBox;
            this.DataContext = viewModel;
            inputMobileBox.Focus();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), BaseViewModel.CurrentWindow, NavigationServiceHelper.TargetWindow.HomePage);
        }
    }
}
