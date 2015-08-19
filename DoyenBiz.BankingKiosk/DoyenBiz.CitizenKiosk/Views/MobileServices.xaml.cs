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
            MobileServiceProvider_4_1,
            MobileServiceProvider_4_2,
            MobileServiceProvider_4_3,
            MobileServiceProvider_4_4
        }

        public MobileServices()
        {
            InitializeComponent();

            m_TimerCollection = new Dictionary<string, TransitionDispatcher>();
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_1.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_1_1, TransitionTiles.MobileServiceProvider_1_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_1_1_logo.jpg", "MobileServiceProvider_1_1_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_2.ToString(),
                        new TransitionDispatcher(MobileServiceProvider_1_2, TransitionTiles.MobileServiceProvider_1_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                      "MobileServiceProvider_1_2_logo.jpg", "MobileServiceProvider_1_2_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_1_3.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_1_3, TransitionTiles.MobileServiceProvider_1_3.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_1_3_logo.jpg", "MobileServiceProvider_1_3_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_1.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_2_1, TransitionTiles.MobileServiceProvider_2_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_2_1_logo.jpg", "MobileServiceProvider_2_1_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_2.ToString(),
                     new TransitionDispatcher(MobileServiceProvider_2_2, TransitionTiles.MobileServiceProvider_2_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_2_2_logo.jpg", "MobileServiceProvider_2_2_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_3.ToString(),
                   new TransitionDispatcher(MobileServiceProvider_2_3, TransitionTiles.MobileServiceProvider_2_3.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "MobileServiceProvider_2_3_logo.jpg", "MobileServiceProvider_2_3_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_2_4.ToString(),
                   new TransitionDispatcher(MobileServiceProvider_2_4, TransitionTiles.MobileServiceProvider_2_4.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "MobileServiceProvider_2_4_logo.jpg", "MobileServiceProvider_2_4_text.jpg", TransitionDispatcher.TileSize.XL));
            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_3_1.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_3_1, TransitionTiles.MobileServiceProvider_3_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_3_1_logo.jpg", "MobileServiceProvider_3_1_text.jpg", TransitionDispatcher.TileSize.Large));


            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_3_2.ToString(),
                  new TransitionDispatcher(MobileServiceProvider_3_2, TransitionTiles.MobileServiceProvider_3_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                   "uMobileServiceProvider_3_2_logo.jpg", "MobileServiceProvider_3_2_text.jpg", TransitionDispatcher.TileSize.Small));

            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_4_1.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_4_1, TransitionTiles.MobileServiceProvider_4_1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_4_1_logo.jpg", "MobileServiceProvider_4_1_text.jpg", TransitionDispatcher.TileSize.Small));

            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_4_2.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_4_2, TransitionTiles.MobileServiceProvider_4_2.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_4_2_logo.jpg", "MobileServiceProvider_4_2_text.jpg", TransitionDispatcher.TileSize.Small));

            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_4_3.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_4_3, TransitionTiles.MobileServiceProvider_4_3.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_4_3_logo.jpg", "MobileServiceProvider_4_3_text.jpg", TransitionDispatcher.TileSize.Large));


            m_TimerCollection.Add(TransitionTiles.MobileServiceProvider_4_4.ToString(),
                    new TransitionDispatcher(MobileServiceProvider_4_4, TransitionTiles.MobileServiceProvider_4_4.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "MobileServiceProvider_4_4_logo.jpg", "MobileServiceProvider_4_4_text.jpg", TransitionDispatcher.TileSize.Small));


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
            transitionInfo.TransTimer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
            //timer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.TransactionHome);
        }
    }
}
