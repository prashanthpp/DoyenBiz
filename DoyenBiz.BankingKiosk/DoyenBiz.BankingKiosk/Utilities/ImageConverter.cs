using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DoyenBiz.BankingKiosk.Utilities
{
    public static class ImageConverter
    {
        public static string ConvertToBase64(string imagePath)
        {
            System.Drawing.Image inputImage = Image.FromFile(imagePath);
            MemoryStream ms = new MemoryStream();
            inputImage.Save(ms, inputImage.RawFormat);
            byte[] imageByteArray = ms.ToArray();

            return Convert.ToBase64String(imageByteArray);
        }

        public static string EncodeInHTML(string inputString)
        {
            return HttpUtility.UrlEncode(inputString);
        }

        //public static 
    }
}
