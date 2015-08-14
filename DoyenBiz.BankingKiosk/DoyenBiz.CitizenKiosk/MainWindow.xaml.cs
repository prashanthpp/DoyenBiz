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
            AIXS,
            GNI,
            BankOfIndia,
            CICIC,
            DBI,
            HFDC,
            HSBC,
            Kanara,
            Telangana,
            Uinon,

            Alliance,
            DEUTSCHE,
            india_oversi,
            IndusInd,
            marudhara,
            ocbc,
            orporation,
            patelco,
            srb,
            srb1,

        }

        public MainWindow()
        {
            InitializeComponent();

            m_TimerCollection = new Dictionary<string, TransitionDispatcher>();
            m_TimerCollection.Add(TransitionTiles.AIXS.ToString(),
                     new TransitionDispatcher(AIXS, TransitionTiles.AIXS.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "axis_bank.jpg", "aixs_bank.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.GNI.ToString(),
                        new TransitionDispatcher(GNI, TransitionTiles.GNI.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                      "gni_logo.jpg", "gni_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.Telangana.ToString(),
                     new TransitionDispatcher(Telangana, TransitionTiles.Telangana.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "telangana_bank_logo.jpg", "telangana_bank_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.HFDC.ToString(),
                     new TransitionDispatcher(HFDC, TransitionTiles.HFDC.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "hfdc_bank.jpg", "hfdc_bank_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.CICIC.ToString(),
                     new TransitionDispatcher(CICIC, TransitionTiles.CICIC.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "cicic_logo.jpg", "cicic.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.Kanara.ToString(),
                   new TransitionDispatcher(Kanara, TransitionTiles.Kanara.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "kanara_logo.jpg", "kanara_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.BankOfIndia.ToString(),
                   new TransitionDispatcher(BankOfIndia, TransitionTiles.BankOfIndia.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                    "bank_of_india_logo.jpg", "bank_of_india-text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.HSBC.ToString(),
                    new TransitionDispatcher(HSBC, TransitionTiles.HSBC.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "hsbc_logo.jpg", "hsbc_text.jpg", TransitionDispatcher.TileSize.Small));


            m_TimerCollection.Add(TransitionTiles.Uinon.ToString(),
                  new TransitionDispatcher(Uinon, TransitionTiles.Uinon.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                   "uinon_logo.jpg", "uinon_logo.jpg", TransitionDispatcher.TileSize.Small));

            m_TimerCollection.Add(TransitionTiles.DBI.ToString(),
                    new TransitionDispatcher(DBI, TransitionTiles.DBI.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "dbi_logo.jpg", "dbi_text.jpg", TransitionDispatcher.TileSize.Small));

            m_TimerCollection.Add(TransitionTiles.Alliance.ToString(),
                    new TransitionDispatcher(Alliance, TransitionTiles.Alliance.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "Alliance_bank_logo.jpg", "Alliance_bank_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.DEUTSCHE.ToString(),
                    new TransitionDispatcher(DEUTSCHE, TransitionTiles.DEUTSCHE.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "DEUTSCHE_Logo.jpg", "DEUTSCHE_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.india_oversi.ToString(),
                    new TransitionDispatcher(india_oversi, TransitionTiles.india_oversi.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "india_oversi_bank_logo.jpg", "india_oversi_bank_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.IndusInd.ToString(),
                    new TransitionDispatcher(IndusInd, TransitionTiles.IndusInd.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "IndusInd_bank_logo.jpg", "IndusInd_bank_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.marudhara.ToString(),
                    new TransitionDispatcher(marudhara, TransitionTiles.marudhara.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "marudhara_logo.jpg", "marudhara_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.ocbc.ToString(),
                    new TransitionDispatcher(ocbc, TransitionTiles.ocbc.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "ocbc.jpg", "ocbc_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.orporation.ToString(),
                    new TransitionDispatcher(orporation, TransitionTiles.orporation.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "orporation.jpg", "orporation_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.patelco.ToString(),
                    new TransitionDispatcher(patelco, TransitionTiles.patelco.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "patelco_logo.jpg", "patelco_text.jpg", TransitionDispatcher.TileSize.Large));
            m_TimerCollection.Add(TransitionTiles.srb.ToString(),
                    new TransitionDispatcher(srb, TransitionTiles.srb.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "srb_logo.jpg", "srb_text.jpg", TransitionDispatcher.TileSize.Small));
            m_TimerCollection.Add(TransitionTiles.srb1.ToString(),
                    new TransitionDispatcher(srb1, TransitionTiles.srb1.ToString(), new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.Normal, Transition, this.Dispatcher),
                     "srb_logo.jpg", "srb_text.jpg", TransitionDispatcher.TileSize.Small));



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
            else
            {
                tile.Width = 120;
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
            transitionInfo.TransTimer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
            //timer.Interval = new TimeSpan(0, 0, rndInterval.Next(5));
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            NavigationServiceHelper.Navigate((sender as Button), this, NavigationServiceHelper.TargetWindow.MobileServices);
        }
    }
}
