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
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoyenBiz.BankingKiosk.Views
{
    /// <summary>
    /// Interaction logic for WithdrawCash.xaml
    /// </summary>
    public partial class WithdrawCash : MetroWindow
    {

        private int m_progress;

        public int Progress
        {
            get { return m_progress; }
            set
            {
                m_progress = value;
                OnPropertyChanged("Progress");
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

    }
}
