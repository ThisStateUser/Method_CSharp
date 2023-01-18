using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Pages.SystemPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using MahApps.Metro.IconPacks;
using System.Windows.Threading;

namespace MethodHelper.Views
{
    /// <summary>
    /// Логика взаимодействия для ImageView.xaml
    /// </summary>

    public partial class ImageView : Window
    {
        List<image_in_page> images = new List<image_in_page>();
        string pageName;
        
        public ImageView(string winname)
        {
            InitializeComponent();
            
            Win.imageView = this;
            ImageWin.Title = winname;
            pageName = winname;
            images = Connect.data.image_in_page.Where(x => x.page_id == Connect.data.page_desc.Where(z => z.title == winname).FirstOrDefault().id).ToList();

            if (Connect.user.role_id != 3)
            {
                addImg.Visibility = Visibility.Visible;
            }

            if (images.Count == 0)
            {
                CollectionImg.HorizontalAlignment = HorizontalAlignment.Center;
                CollectionImg.VerticalAlignment = VerticalAlignment.Center;

                TextBlock textBlock = new TextBlock()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = "Изображений нет :(",
                };

                CollectionImg.Children.Add(textBlock);
                return;
            }
            showimg();
        }

        private void addImg_Click(object sender, RoutedEventArgs e)
        {
            ImgFrame.Navigate(new AddImagePage(pageName));
        }

        public void showimg()
        {
            xamlZone.Children.Clear();
            csZone.Children.Clear();
            dbZone.Children.Clear();
            otherZone.Children.Clear();

            foreach (var img in images)
            {
                switch (img.type_id)
                {
                    case 1:
                        s_xaml.Visibility = Visibility.Visible;
                        generateZoneImg(xamlZone, img.image, img.description);
                        break;
                    case 2:
                        s_cs.Visibility = Visibility.Visible;
                        generateZoneImg(csZone, img.image, img.description);
                        break;
                    case 3:
                        s_db.Visibility = Visibility.Visible;
                        generateZoneImg(dbZone, img.image, img.description);
                        break;
                    case 4:
                        s_other.Visibility = Visibility.Visible;
                        generateZoneImg(otherZone, img.image, img.description);
                        break;
                    default:
                        MessageBox.Show("Неизвестная ошибка");
                        break;
                }
            }
        }

        private void generateZoneImg(WrapPanel zone, byte[] img, string content)
        {
            Border border = new Border()
            {
                Style = (Style)FindResource("ImgBorder"),
                Width = (double)138,
                Height = (double)138,
            };
            border.MouseDown += (o, e) => Border_MouseDown(o, e);

            StackPanel stackPanel = new StackPanel();

            BitmapImage preview = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream(img);
            preview.BeginInit();
            preview.StreamSource = memoryStream;
            preview.EndInit();

            Image image = new Image()
            {
                Source = preview,
            };

            TextBlock desc = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                Visibility = Visibility.Collapsed,
                Text = content,
            };

            zone.Children.Add(border);
            border.Child = stackPanel;
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(desc);
        }


        Border open_img;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border br = (Border)sender;
            TextBlock img_desc = (TextBlock)((StackPanel)br.Child).Children[1];
            if (open_img == br)
            {
                br.Width = (double)138;
                br.Height = (double)138;
                img_desc.Visibility = Visibility.Collapsed;
                open_img = null;
                return;
            }
            if (open_img != br && open_img != null)
            {
                open_img.Width = (double)138;
                open_img.Height = (double)138;
                ((TextBlock)((StackPanel)open_img.Child).Children[1]).Visibility = Visibility.Collapsed;
                open_img = br;
            }
            br.Width = sliderImg.Value - 10;
            br.Height = double.NaN;
            open_img = br;
            img_desc.Visibility = Visibility.Visible;
        }

        private void ImageWin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sliderImg.Value > ActualWidth)
            { 
                CollectionImg.Width = sliderImg.Value + 10;
            } else
            {
                CollectionImg.Width = ActualWidth;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int slide = Convert.ToInt32(sliderImg.Value);
            snapPixel.Text = slide.ToString() + " px";
            if (CollectionImg != null && sliderImg.Value > ActualWidth)
            {
                CollectionImg.Width = sliderImg.Value;
            }
            if (open_img != null)
            {
                open_img.Width = sliderImg.Value - 10;
            }
        }
    }
}
