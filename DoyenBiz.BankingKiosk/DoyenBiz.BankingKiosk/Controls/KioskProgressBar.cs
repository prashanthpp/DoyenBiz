using System.Windows;
using System.Windows.Controls;

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
