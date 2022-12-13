using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MethodHelper.Pages.PrefComElement
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        public Basket()
        {
            InitializeComponent();
        }

        private void BasketBtn_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            var file = new OpenFileDialog();
            file.Filter = "Изображение Img|*.png;*.jpg";
            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                BitmapImage image = new BitmapImage();
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();


            }
        }
    }
}
