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
using MahApps.Metro.Controls.Dialogs;
using DoyenBiz.CitizenKiosk.Views;
using DoyenBiz.CitizenKiosk.Utilities;
using System.Windows.Threading;
using DoyenBiz.CitizenKiosk.Common;
using DoyenBiz.CitizenKiosk.Utilities;

namespace DoyenBiz.CitizenKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Dictionary<string, TransitionDispatcher> m_TimerCollection;
        //DispatcherTimer timer;
        Random rndInterval;


        public enum TransitionTiles
        {
            mobile,
            utility,
            wheeler,
            traffic,
          

        }

        public MainWindow()
        {
            InitializeComponent();

            m_TimerCollection = new Dictionary<string, TransitionDispatcher>();
            m_TimerCollection.Add(TransitionTiles.mobile.ToString(),
                     new TransitionDispatcher(mobile, TransitionTiles.mobile.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "mobile_services.jpg", "mobile_services1.jpg", TransitionDispatcher.TileSize.Large));

            m_TimerCollection.Add(TransitionTiles.utility.ToString(),
                        new TransitionDispatcher(utility, TransitionTiles.utility.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                      "Pay_utlity_bills.jpg", "Pay_utlity_bills1.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.wheeler.ToString(),
                     new TransitionDispatcher(wheeler, TransitionTiles.wheeler.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "four_wheeler_insu.jpg", "two_wheeler_ins.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.traffic.ToString(),
                     new TransitionDispatcher(traffic, TransitionTiles.traffic.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "Police_E_Chalana.jpg", "Police_E_Chalana1.jpg", TransitionDispatcher.TileSize.Large));
         

            var txtDispatcher = new DispatcherTimer(TimeSpan.FromSeconds(3), DispatcherPriority.Normal, TransitionText, this.Dispatcher);
            var txtDispatcher1 = new DispatcherTimer(TimeSpan.FromSeconds(4), DispatcherPriority.Normal, TransitionText1, this.Dispatcher);
            var txtDispatcher2 = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, TransitionText2, this.Dispatcher);
            var txtDispatcher3 = new DispatcherTimer(TimeSpan.FromSeconds(4), DispatcherPriority.Normal, TransitionText3, this.Dispatcher);

            rndInterval = new Random();
        }

        private void bank_click(object sender, RoutedEventArgs e)
        {
        }

        void Transition(object sender, EventArgs e)
        {
            var transitionInfo = m_TimerCollection[((DispatcherTimer)sender).Tag.ToString()];
            var tile = new Tile();
            tile.Click += Tile_Click;
            if (transitionInfo.TransTileSize == TransitionDispatcher.TileSize.Large)
            {
                tile.Width = 265;
                tile.Height = 125;
            }
            else
            {
                tile.Width = 133;
                tile.Height = 125;
            }
            var transitioning = transitionInfo.TransControl;
            if (transitioning.Content == null || ((Tile)transitioning.Content).Tag.ToString() == "Text")
            {

                //tile.TitleFontSize = 10;
                tile.Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + string.Format("/Resources/Images/{0}", transitionInfo.ImageLogo), UriKind.Relative)));
                tile.Tag = "Logo";
            }
            else
            {
                tile.Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + string.Format("/Resources/Images/{0}", transitionInfo.ImageText), UriKind.Relative)));
                tile.Tag = "Text";
            }
            transitioning.Content = tile;// new BitmapImage(new Uri(""));
            transitionInfo.TransTimer.Interval = new TimeSpan(0, 0, rndInterval.Next(3,8));
            //timer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
        }


        void TransitionText(object sender, EventArgs e)
        {
            var txtContent = new TextBlock();
            txtContent.TextWrapping = TextWrapping.Wrap;
            txtContent.Width = 180;
            txtContent.HorizontalAlignment = HorizontalAlignment.Left;
            txtContent.FontFamily = new FontFamily("Segoe UI");
            txtContent.FontSize = 15;
            txtContent.TextAlignment = TextAlignment.Justify;
            txtContent.Margin = new Thickness(5, 5, 5, 5);


            if (about.Tag.ToString() == "Text")
            {
                txtContent.Text = "CUSTOMIZE this Kiosk! Create your unique Kiosk PIN to access all your bill payments and frequently used services at one place! At any KIOSK!";
                about.Content = txtContent;
                about.Tag = "Logo";
            }
            else
            {
                txtContent.Text = "This ATM uses a two-factor Biometric Authentication to check you are the account holder. By continuing to use the ATM services, you agree to provide you finger print and you agree to have your picture taken";
                about.Content = txtContent;
                about.Tag = "Text";
            }
        }


        void TransitionText1(object sender, EventArgs e)
        {
            var txtContent = new TextBlock();
            txtContent.TextWrapping = TextWrapping.Wrap;
            txtContent.Width = 80;
            txtContent.HorizontalAlignment = HorizontalAlignment.Left;
            txtContent.FontFamily = new FontFamily("Segoe UI");
            txtContent.FontSize = 15;
            txtContent.TextAlignment = TextAlignment.Justify;
            txtContent.Margin = new Thickness(2,2,2,2);


            txtContent.Text = "Your Feedback is valuable to us";
            feedback.Content = txtContent;
        }


        void TransitionText2(object sender, EventArgs e)
        {
            var txtContent = new TextBlock();
            txtContent.TextWrapping = TextWrapping.Wrap;
            txtContent.Width = 80;
            txtContent.HorizontalAlignment = HorizontalAlignment.Left;
            txtContent.FontFamily = new FontFamily("Segoe UI");
            txtContent.FontSize = 15;
            txtContent.TextAlignment = TextAlignment.Justify;
            txtContent.Margin = new Thickness(2,2,2,2);


            txtContent.Text = "DoyenBiz - About Us";
            aboutUs.Content = txtContent;
        }


        void TransitionText3(object sender, EventArgs e)
        {
            var txtContent = new TextBlock();
            txtContent.TextWrapping = TextWrapping.Wrap;
            txtContent.Width = 210;
            txtContent.HorizontalAlignment = HorizontalAlignment.Left;
            txtContent.FontFamily = new FontFamily("Segoe UI");
            txtContent.FontSize = 15;
            txtContent.TextAlignment = TextAlignment.Justify;
            txtContent.Margin = new Thickness(2, 2, 2, 2);


            txtContent.Text = "Need Help Or support? Call us on 1-800-BANKING-KIOSK";
            needHelp.Content = txtContent;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.MobileServices);
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.MobileServices);
        }
    }
}
