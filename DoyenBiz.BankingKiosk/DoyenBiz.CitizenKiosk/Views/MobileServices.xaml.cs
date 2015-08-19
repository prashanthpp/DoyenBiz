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

namespace DoyenBiz.CitizenKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MobileServices : MetroWindow
    {
        Dictionary<string, TransitionDispatcher> m_TimerCollection;
        //DispatcherTimer timer;
        Random rndInterval;


        public enum TransitionTiles
        {
            MobileServiceProvider_1_1,
            MobileServiceProvider_1_2,
            MobileServiceProvider_1_3,
            MobileServiceProvider_2_1,
            MobileServiceProvider_2_2,
            MobileServiceProvider_2_3,
            MobileServiceProvider_2_4,
            MobileServiceProvider_3_1,
            MobileServiceProvider_3_2,
   
        }

        public MobileServices()
        {
            InitializeComponent();

            m_TimerCollection = new Dictionary<string, TransitionDispatcher>();
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_1.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_1_1, TransitionTiles.MobileServiceProvider_1_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "airtel.jpg", "airtel1.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_2.ToString(),
                        new TransitionDispatcher(MobileServiceProvider_1_2, TransitionTiles.MobileServiceProvider_1_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                      "uninor.jpg", "uninor1.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_3.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_1_3, TransitionTiles.MobileServiceProvider_1_3.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "aircel.jpg", "aircel1.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_1.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_2_1, TransitionTiles.MobileServiceProvider_2_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "idea.jpg", "idea1.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_2.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_2_2, TransitionTiles.MobileServiceProvider_2_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "tata_indicom.jpg", "tata_indicom1.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_3.ToString(),
                   new TransitionDispatcher(MobileServiceProvider_2_3, TransitionTiles.MobileServiceProvider_2_3.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "reliance.jpg", "reliance1.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_4.ToString(),
                   new TransitionDispatcher(MobileServiceProvider_2_4, TransitionTiles.MobileServiceProvider_2_4.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "Vodafone.jpg", "Vodafone1.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_3_1.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_3_1, TransitionTiles.MobileServiceProvider_3_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "bsnl_logo.jpg", "bsnl_logo1.jpg", TransitionDispatcher.TileSize.Small));



            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_3_2.ToString(),
                  new TransitionDispatcher(MobileServiceProvider_3_2, TransitionTiles.MobileServiceProvider_3_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                   "tata_docomo.jpg", "tata_docomo1.jpg", TransitionDispatcher.TileSize.Large));



            rndInterval = new Random();
        }

        private void bank_click(object sender, RoutedEventArgs e)
        {
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.HomePage);
        }

        void Transition(object sender, EventArgs e)
        {
            var transitionInfo = m_TimerCollection[((DispatcherTimer)sender).Tag.ToString()];
            var tile = new Tile();
            tile.Click += Tile_Click;
            if (transitionInfo.TransTileSize == TransitionDispatcher.TileSize.Large)
            {
                tile.Width = 240;
                tile.Height = 125;
            }
            else if(transitionInfo.TransTileSize == TransitionDispatcher.TileSize.Small)
            {
                tile.Width = 120;
                tile.Height = 125;
            }
            else if (transitionInfo.TransTileSize == TransitionDispatcher.TileSize.XL)
            {
                tile.Width = 240;
                tile.Height = 250;
            }
            var transitioning = transitionInfo.TransControl;
            if (transitioning.Content == null || ((Tile)transitioning.Content).ToolTip.ToString() == "Text")
            {

                //tile.TitleFontSize = 10;
                tile.Tag = Environment.CurrentDirectory + string.Format("/Resources/Images/{0}", transitionInfo.ImageLogo);
                tile.Background = new ImageBrush(new BitmapImage(new Uri(tile.Tag.ToString(), UriKind.Relative)));
                tile.ToolTip = "Logo";
            }
            else
            {
                tile.Tag = Environment.CurrentDirectory + string.Format("/Resources/Images/{0}", transitionInfo.ImageText);
                tile.Background = new ImageBrush(new BitmapImage(new Uri(tile.Tag.ToString(), UriKind.Relative)));
                tile.ToolTip = "Text";
            }
            transitioning.Content = tile;// new BitmapImage(new Uri(""));
            transitionInfo.TransTimer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
            //timer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            TransactionHome.SelectedProvider = ((Tile)sender).Tag.ToString().Replace(Environment.CurrentDirectory, string.Empty);
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.TransactionHome);
        }
       

    }
}
