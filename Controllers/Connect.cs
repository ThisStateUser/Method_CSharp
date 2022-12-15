using MethodHelper.BD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MethodHelper.Controllers
{
    class Connect
    {
        public static Model1 data;

        public static users user;

        public static string host;
        public static string ip;
    }
}

//public ImageSource picture
//{
//    get
//    {
//        if (row_image != null)
//        {
//            BitmapImage bitmap = new BitmapImage();
//            MemoryStream memoryStream = new MemoryStream(row_image);

//            bitmap.BeginInit();
//            bitmap.StreamSource = memoryStream;
//            bitmap.EndInit();

//            return bitmap as ImageSource;
//        }
//        BitmapImage notAviable = new BitmapImage();
//        MemoryStream memoryStream1 = new MemoryStream(File.ReadAllBytes(@"..\..\Resources\Images\not-available.png"));

//        notAviable.BeginInit();
//        notAviable.DecodePixelWidth = 64;
//        notAviable.StreamSource = memoryStream1;
//        notAviable.EndInit();
//        return notAviable as ImageSource;
//    }
//}
