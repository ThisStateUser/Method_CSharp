namespace MethodHelper.BD
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class image_in_page
    {
        public int id { get; set; }

        public int page_id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] image { get; set; }

        public ImageSource picture
        {
            get
            {
                BitmapImage bitmap = new BitmapImage();
                MemoryStream memoryStream = new MemoryStream(image);

                bitmap.BeginInit();
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();

                return bitmap as ImageSource;
            }
        }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public int type_id { get; set; }

        public virtual page_desc page_desc { get; set; }

        public virtual type_image type_image { get; set; }
    }
}
