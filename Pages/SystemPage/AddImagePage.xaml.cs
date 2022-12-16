using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Views;
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

namespace MethodHelper.Pages.SystemPage
{
    /// <summary>
    /// Логика взаимодействия для AddImagePage.xaml
    /// </summary>
    public partial class AddImagePage : Page
    {
        byte[] _imgs;
        int _page_id;
        public AddImagePage(string winname)
        {
            InitializeComponent();
            var selectPage = Connect.data.page_desc.Where(x => x.title == winname).FirstOrDefault();
            Win.imageView.ImgFrame.Visibility = Visibility.Visible;
            if (selectPage == null)
            {
                MessageBoxResult result = MessageBox.Show("Страница " + winname + " отсутствует в базе данных, вы хотите создать ее?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        page_desc page_Desc = new page_desc()
                        {
                            title = winname,
                        };
                        Connect.data.page_desc.Add(page_Desc);
                        Connect.data.SaveChanges();
                        
                    }
                    catch (Exception ex)
                    {
                        WinObj.fatalError(ex);
                    }
                } else
                {
                    Win.imageView.ImgFrame.Visibility = Visibility.Collapsed;
                }
                return;
            }
            _page_id = selectPage.id;
            t_page_id.Text = "Добавление изображения для страницы: " + winname;
            cb_type.ItemsSource = Connect.data.type_image.ToList();
            scrollbase.Height = Win.imageView.ActualHeight;
        }

        private void selectImg_Click(object sender, RoutedEventArgs e)
        {
            selectImg.IsEnabled = false;
            Task.Run(() => { asyncImg(); });
        }

        private void asyncImg()
        {
            var pathImage = new OpenFileDialog();
            pathImage.Filter = "img|*.png;*.jpeg;*.jpg";
            var result = pathImage.ShowDialog();
            if (result != null && result == true)
            {
                var imgdata = File.ReadAllBytes(pathImage.FileName);
                Dispatcher.Invoke(() =>
                {
                    selectImg.Content = pathImage.FileName;
                    _imgs = imgdata;
                    selectImg.IsEnabled = true;                    
                    
                    BitmapImage preview = new BitmapImage();
                    MemoryStream memoryStream = new MemoryStream(imgdata);
                    preview.BeginInit();
                    preview.StreamSource = memoryStream;
                    preview.EndInit();
                    preview_img.Source = preview;
                });
            }
            else if (result == false)
            {
                Dispatcher.Invoke(() =>
                {
                    selectImg.IsEnabled = true;
                });
            }
        }

        private void sendImg_Click(object sender, RoutedEventArgs e)
        {
            if (_imgs == null || cb_type.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля помеченные \"*\"", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            try
            {
                image_in_page image_In_Page = new image_in_page()
                {
                    page_id = _page_id,
                    image = _imgs,
                    type_id = ((type_image)cb_type.SelectedItem).id,
                };
                if (desc_img.Text.Length != 0)
                {
                    image_In_Page.description = desc_img.Text;
                }
                Connect.data.image_in_page.Add(image_In_Page);
                Connect.data.SaveChanges();
                MessageBox.Show("Изображение добавлено", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                goBack_Click(null, null);
                Win.imageView.showimg();
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Win.imageView.ImgFrame.Visibility = Visibility.Collapsed;
            Win.imageView.showimg();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scrollbase.Height = ActualHeight - 5;
        }
    }
}
