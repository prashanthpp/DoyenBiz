using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoyenBiz.BankingKiosk.Controls
{
    public class KioskProgressBar : ItemsControl
    {

        public static DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(int), typeof(KioskProgressBar), new FrameworkPropertyMetadata(0, null, WizardProgress));

        private static object WizardProgress(DependencyObject target, object value)
        {
            KioskProgressBar kioskProgressBar = (KioskProgressBar)target;
            int progress = (int)value;
            if (progress < 0)
            {
                progress = 0;
            }
            else if (progress > 100)
            {
                progress = 100;
            }
            return progress;
        }


        static KioskProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KioskProgressBar), new FrameworkPropertyMetadata(typeof(KioskProgressBar)));
        }

        public KioskProgressBar()
        {
        }


        public int Progress
        {
            get { return (int)base.GetValue(ProgressProperty); }
            set { base.SetValue(ProgressProperty, value); }
        }

        
    }
}
