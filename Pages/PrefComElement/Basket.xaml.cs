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
using MethodHelper.Controllers;
using System.Windows.Threading;

namespace MethodHelper.Pages.PrefComElement
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Basket()
        {
            InitializeComponent();
            Lv_magazine.ItemsSource = Connect.data.product.ToList();
            Filter.ItemsSource = Connect.data.category.ToList();

            Filter.SelectedIndex = 0;
            Sorting.SelectedIndex = 0;


            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick; ;

            WinObj.deskHelp(s_description, description, Title);
            if (Connect.user.role_id != 3)
            {
                addDesc.Visibility = Visibility.Visible;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            BD.Model1.UpdateContext();
            Update();
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            WinObj.ShowCode(Title);
        }

        private void addDesc_Click(object sender, RoutedEventArgs e)
        {
            WinObj.addDesc(addDesc, description, textbox_desc, s_description, Title);
            WinObj.deskHelp(s_description, description, Title);
        }

        private void Update()
        {
            var product = BD.Model1.GetContext().product.ToList();

            switch (Sorting.SelectedIndex)
            {
                case 1:
                    product = product.OrderByDescending(s => s.price).ToList();
                    break;
                case 2:
                    product = product.OrderBy(s => s.price).ToList();
                    break;
                default:
                    break;
            }

            if (Filter.SelectedIndex == 1)
            {
                product = product.Where(x => x.category_id == 2).ToList();
            }
            if (Filter.SelectedIndex == 2)
            {
                product = product.Where(x => x.category_id == 3).ToList();
            }

            product = product.Where(x => x.title.ToLower().Trim().Contains(SearchMB.Text.ToLower().Trim())).ToList();

            Lv_magazine.ItemsSource = product;
        }

        private void StoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Lv_magazine.Visibility = Visibility.Visible;
            Lv_Basket.Visibility = Visibility.Collapsed;
        }

        private void BasketBtn_Click(object sender, RoutedEventArgs e)
        {
            Lv_magazine.Visibility = Visibility.Collapsed;
            Lv_Basket.Visibility = Visibility.Visible;
            //string path = "";
            //var file = new OpenFileDialog();
            //file.Filter = "Изображение Img|*.png;*.jpg";
            //if (file.ShowDialog() == true)
            //{
            //    path = file.FileName;
            //    BitmapImage image = new BitmapImage();
            //    MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
            //    image.BeginInit();
            //    image.StreamSource = ms;
            //    image.EndInit();
            //}
        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
