using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoyenBiz.BankingKiosk.ViewModels
{
    public class BaseViewModel
    {
        public static MetroWindow CurrentWindow;

        public BaseViewModel()
        {
            InitializeVariables();
        }

        private ICommand m_ButtonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return m_ButtonCommand;
            }
            set
            {
                m_ButtonCommand = value;
            }
        }


        public void ToggleFlyout(int index)
        {
            var flyout = CurrentWindow.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }


        private int m_progress;


        public ObservableCollection<string> Steps
        {
            get;
            set;
        }


        public int Progress
        {
            get { return m_progress; }
            set
            {
                m_progress = value;
                OnPropertyChanged("Progress");
            }
        }

        private void InitializeVariables()
        {
            Steps = new ObservableCollection<string>();
            Steps.Add("Swipe Card");
            Steps.Add("Enter Pin");
            Steps.Add("Biometric Authentication");
            Steps.Add("Amount to Withdraw");
            Steps.Add("Take Cash");
            // this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
