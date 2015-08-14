using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DoyenBiz.CitizenKiosk.Common
{
    public class TransitionDispatcher
    {
        public enum TileSize
        {
            Large,
            Small,
            XL
        }
        public DispatcherTimer TransTimer
        {
            get;
            set;
        }

        public string ImageLogo
        {
            get;
            set;
        }

        public string ImageText
        {
            get;
            set;
        }

        public TransitioningContentControl TransControl
        {
            get;
            set;
        }

        public TileSize TransTileSize
        {
            get;
            set;
        }

        public TransitionDispatcher(TransitioningContentControl transCtrl,string dispatcherName, DispatcherTimer timer, string imageLogo, string imageText , TileSize transTileSize )
        {
            TransTimer = timer;
            ImageLogo = imageLogo;
            ImageText = imageText;
            TransControl = transCtrl;
            TransTileSize = transTileSize;
            TransTimer.Tag = dispatcherName;
        }
    }
}
