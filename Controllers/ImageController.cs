using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MethodHelper.BD;

namespace MethodHelper.Controllers
{
    internal class ImageController
    {
        //public ImageSource method_crud_picture
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

        //public ImageSource image_in_page_picture
        //{
        //    get
        //    {
        //        if (image == null)
        //        { return null; }
        //        BitmapImage bitmap = new BitmapImage();
        //        MemoryStream memoryStream = new MemoryStream(image);

        //        bitmap.BeginInit();
        //        bitmap.StreamSource = memoryStream;
        //        bitmap.EndInit();

        //        return bitmap as ImageSource;
        //    }
        //}

        //public ImageSource product_picture
        //{
        //    get
        //    {
        //        if (image == null)
        //        {
        //            BitmapImage notAviable = new BitmapImage();
        //            MemoryStream memoryStream1 = new MemoryStream(File.ReadAllBytes(@"..\..\Resources\Images\not-available.png"));

        //            notAviable.BeginInit();
        //            notAviable.DecodePixelWidth = 64;
        //            notAviable.StreamSource = memoryStream1;
        //            notAviable.EndInit();
        //            return notAviable as ImageSource;
        //        }
        //        BitmapImage bitmap = new BitmapImage();
        //        MemoryStream memoryStream = new MemoryStream(image);

        //        bitmap.BeginInit();
        //        bitmap.StreamSource = memoryStream;
        //        bitmap.EndInit();

        //        return bitmap as ImageSource;
        //    }
        //}
    }
}
