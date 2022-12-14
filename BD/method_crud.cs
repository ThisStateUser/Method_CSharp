namespace MethodHelper.BD
{
    using System.Windows.Media.Imaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows.Media;
    using System.IO;
    public partial class method_crud
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string row_text { get; set; }

        public int? row_int { get; set; }

        public bool? row_bool { get; set; }

        [Column(TypeName = "image")]
        public byte[] row_image { get; set; }
        public ImageSource picture
        {
            get
            {
                if (row_image != null)
                {
                    BitmapImage bitmap = new BitmapImage();
                    MemoryStream memoryStream = new MemoryStream(row_image);

                    bitmap.BeginInit();
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();

                    return bitmap as ImageSource;
                }
                BitmapImage notAviable = new BitmapImage();
                MemoryStream memoryStream1 = new MemoryStream(File.ReadAllBytes(@"..\..\Resources\Images\not-available.png"));

                notAviable.BeginInit();
                notAviable.DecodePixelWidth = 64;
                notAviable.StreamSource = memoryStream1;
                notAviable.EndInit();
                return notAviable as ImageSource;
            }
        }

        public int? row_combo { get; set; }

        public virtual method_crud_combobox method_crud_combobox { get; set; }
    }
}






